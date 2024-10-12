using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotifiNoteBE.DTO;
using NotifiNoteBE.Repostries;

namespace NotifiNoteBE.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserRepo _userRepo;

		public AccountController(IUserRepo userRepo)
        {
			_userRepo = userRepo;
		}
        [HttpPost]
		public async Task<IActionResult> Register([FromBody]UserRegister user) { 
		
			var resault = await _userRepo.Register(user);
			if (resault == null)
			{
				return BadRequest();
			}
			return Ok(resault);
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(UserSignIn user) { 
		
			var resault = await _userRepo.SignIn(user);
			if (resault == null)
			{
				return BadRequest();
			}
			return Ok(resault);
		}
	}
}
