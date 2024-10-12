using NotifiNoteBE.Models;

namespace NotifiNoteBE.DTO
{
	public class NoteDto
	{
		public int? Id { get; set; } = null;
		public string Title { get; set; }
		public string? Description { get; set; } = null;
		public DateTime? Alarm { get; set; } = null;
		//public string UserId { get; set; }
	}
}
