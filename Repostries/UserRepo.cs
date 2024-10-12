using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotifiNoteBE.Configurations;
using NotifiNoteBE.Data;
using NotifiNoteBE.DTO;
using NotifiNoteBE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotifiNoteBE.Repostries
{
	public class UserRepo : IUserRepo
	{
		private readonly NotifiNoteDB _db;
		private readonly UserManager<User> _userMg;
		private readonly SignInManager<User> _signIn;
		private readonly IOptions<JwtConfig> _jwtConfig;

		public UserRepo(NotifiNoteDB Db, UserManager<User> user, SignInManager<User> signIn, IOptions<JwtConfig> jwtConfig)
		{
			_db = Db;
			_userMg = user;
			_signIn = signIn;
			_jwtConfig = jwtConfig;
		}
		public async Task<string?> Register(UserRegister userData)
		{
			var user = new User()
			{
				UserName = userData.UserName,
				Email = userData.Email,
				PhoneNumber = userData.PhoneNumber
			};

			var resault = await _userMg.CreateAsync(user, userData.Password);
			if (!resault.Succeeded)
			{
				return null;
			}
			return "Succeded";
		}
		public async Task<string?> SignIn(UserSignIn userData)
		{
			var resault = await _signIn.PasswordSignInAsync(userData.UserName, userData.Password, false, false);
			if (!resault.Succeeded) return null;
			var user = _db.Users.First(d => d.UserName == userData.UserName);
			var Claims = new List<Claim> {
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Secret));
			var token = new JwtSecurityToken(
				issuer: _jwtConfig.Value.ValidIssuer,
				audience: _jwtConfig.Value.ValidAudience,
				expires: DateTime.UtcNow.AddDays(1),
				claims: Claims,
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
