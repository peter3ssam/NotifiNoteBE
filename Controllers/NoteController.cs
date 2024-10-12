using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotifiNoteBE.DTO;
using NotifiNoteBE.Models;
using NotifiNoteBE.Repostries;
using System.Security.Claims;

namespace NotifiNoteBE.Controllers
{
	
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
	public class NoteController : ControllerBase
	{
		private readonly INoteRepo _noteRepo;

		public NoteController(INoteRepo noteRepo)
        {
			_noteRepo = noteRepo;
		}
        [HttpPost]
		public async Task<IActionResult> CreateNote(NoteDto note)
		{
			var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (note == null|| user==null)
				return BadRequest();
			await  _noteRepo.CreateNote(user,note);
			return Ok("succeded");
		}
		[HttpPut]
		public async Task<IActionResult> UpdateNote(NoteDto note)
		{
			if (note == null)
				return BadRequest();
			var resault = await _noteRepo.UpdateNote(note);
			if(resault == null)
				return BadRequest();
			return Ok("succeded");
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteNote(int noteId)
		{

			var resault = await _noteRepo.DeleteNote(noteId);
			if (resault == null)
				return BadRequest();
			return Ok("succeded");
		}
		[HttpGet]
		public async Task<IActionResult> GetNote(int noteId)
		{
			var resault = await _noteRepo.GetNote(noteId);
			if (resault == null)
				return BadRequest();
			return Ok("succeded");
		}
		[HttpGet]
		public async Task<IActionResult> GetAllNotes()
		{
			var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if(user == null)
				return BadRequest();
			var notes = await _noteRepo.GetAllNotes(user);
			return Ok(notes);
		}
	}
}
