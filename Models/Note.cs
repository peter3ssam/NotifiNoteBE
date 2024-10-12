namespace NotifiNoteBE.Models
{
	public class Note
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; } = null;
		public DateTime? Alarm { get; set; } = null;
		public string UserId {  get; set; }
		public User User { get; set; }
	}
}
