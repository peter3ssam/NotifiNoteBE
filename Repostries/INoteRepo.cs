using NotifiNoteBE.DTO;
using NotifiNoteBE.Models;

namespace NotifiNoteBE.Repostries
{
	public interface INoteRepo
	{
		Task<string> CreateNote(string userId, NoteDto note);
		Task<string> DeleteNote(int noteId);
		Task<List<Note>?> GetAllNotes(string userId);
		Task<Note?> GetNote(int noteId);
		Task<string?> UpdateNote(NoteDto note);
	}
}