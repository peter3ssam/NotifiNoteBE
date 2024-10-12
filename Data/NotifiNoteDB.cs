using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotifiNoteBE.Models;

namespace NotifiNoteBE.Data
{
	public class NotifiNoteDB:IdentityDbContext<User>
	{
		public NotifiNoteDB(DbContextOptions<NotifiNoteDB> options)
	: base(options)
		{
		}
		public DbSet<Note> Notes { get; set; }
	}
}
