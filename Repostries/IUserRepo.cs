using NotifiNoteBE.DTO;

namespace NotifiNoteBE.Repostries
{
	public interface IUserRepo
	{
		Task<string?> Register(UserRegister userData);
		Task<string?> SignIn(UserSignIn userData);
	}
}