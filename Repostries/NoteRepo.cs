using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotifiNoteBE.Configurations;
using NotifiNoteBE.Data;
using NotifiNoteBE.DTO;
using NotifiNoteBE.Models;

namespace NotifiNoteBE.Repostries
{
	public class NoteRepo : INoteRepo
	{
		private readonly NotifiNoteDB _DB;

		public NoteRepo(NotifiNoteDB DB)
		{
			_DB = DB;
		}
		public async Task<string> CreateNote(string userId, NoteDto note)
		{

			Note newNote = new Note() { Title = note.Title, Description = note.Description, Alarm = note.Alarm, UserId = userId };

			await _DB.Notes.AddAsync(newNote);
			await _DB.SaveChangesAsync();
			return "note added succeded";
		}
		public async Task<string?> UpdateNote(NoteDto note)
		{

			var oldNote = await _DB.Notes.FindAsync(note.Id);
			if (oldNote == null || note.Id == null)
			{
				return null;
			}

			oldNote.Title = note.Title;
			oldNote.Description = note.Description;
			oldNote.Alarm = note.Alarm;
			_DB.SaveChanges();

			return "note updated succeded";
		}
		public async Task<string> DeleteNote(int noteId)
		{

			Note note = new Note() { Id = noteId };
			_DB.Notes.Remove(note);
			await _DB.SaveChangesAsync();
			return "note deleted";
		}
		public async Task<Note?> GetNote(int noteId)
		{
			var Note = await _DB.Notes.FindAsync(noteId);
			return Note;
		}
		public async Task<List<Note>?> GetAllNotes(string userId)
		{

			var notes = await _DB.Notes.Where(n=>n.UserId == userId).ToListAsync();
			return notes;
		}
	}
}
