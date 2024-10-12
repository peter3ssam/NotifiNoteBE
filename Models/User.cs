using Microsoft.AspNetCore.Identity;

namespace NotifiNoteBE.Models
{
	public class User:IdentityUser
	{
		public ICollection<Note>? Notes { get; set; } = null;
	}
}
