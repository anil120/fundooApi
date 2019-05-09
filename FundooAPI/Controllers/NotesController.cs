// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using FundooAPI.DataContext;
    using FundooAPI.Models;
    using FundooAPI.Repository;
    using FundooData.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    
    /// <summary>
    /// this is the controller class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The context
        /// </summary>
        private AuthenticationContext context;
        private readonly INotesRepository notesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userManager">The user manager.</param>
        public NotesController(AuthenticationContext context,INotesRepository notesRepository)
        {
            this.context = context;
            this.notesRepository = notesRepository;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        [HttpPost]
        public async Task<IActionResult> PostNotes([FromBody] NotesModel notesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             context.Notes.Add(notesModel);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetNotes", new { id = notesModel.Id }, notesModel);
        }


        [HttpGet("GetNotesLabel/{Id}")]
        public List<NotesLabelTable> GetNotesLabel(int userId)
        {
            var list = new List<NotesLabelTable>();
            var Labeldata = from t in context.tblNotesLabel where t.NoteId == userId select t;
            try
            {
                foreach (var data in Labeldata)
                {
                    list.Add(data);
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return list;
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        // PUT: api/Workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes([FromRoute] Guid id, [FromBody] NotesModel notesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var notes = await context.Notes.SingleOrDefaultAsync(m => m.Id == id);
            notes.Title = notesModel.Title;
            notes.TakeANote = notesModel.TakeANote;
            notes.RemindMe = notesModel.RemindMe;
            notes.Email = notesModel.Email;
            notes.ColorCode = notesModel.ColorCode;
            notes.ImageUrl = notesModel.ImageUrl;
            notes.IsArchieve = notesModel.IsArchieve;
            notes.IsPin = notesModel.IsPin;
            notes.IsTrash = notesModel.IsTrash;
            notes.Reminder = notesModel.Reminder;
            notes.IsPin = notesModel.IsPin;
            
            try
            {
                
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(notesModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
       // [HttpDelete("DeleteNotes/{ID}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await context.Notes.SingleOrDefaultAsync(m => m.Id == id);
            if (notes == null)
            {
                return NotFound();
            }

            context.Notes.Remove(notes);
            await context.SaveChangesAsync();

            return Ok(notes);
        }
        
        [HttpGet]
        public ICollection<NotesModel> GetNotes()
        {
            IList<NotesModel> notesModels= notesRepository.GetNotes().ToList();
            return notesModels;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotes([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var notes = await context.Notes.SingleOrDefaultAsync(m => m.Id == id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }
        private bool NotesExists(Guid id)
        {
            return context.Notes.Any(e => e.Id == id);
        }
        //to get notes
        [HttpGet("GetNotes/{Email}")]
        public object GetNotes(string Email)
        {
            var list = new List<NotesModel>();
            var label = new List<LableTbl>();
            var SharingNote = new List<CollaboratorTbl>();
            var Label = from t in context.tblLabel where t.Email == Email select t;
            foreach (var lbl in Label)
            {
                label.Add(lbl);
            }
            GetNotesData data = new GetNotesData();
            var Notesdata = from t in context.Notes where t.Email == Email select t;

            foreach (var item in Notesdata)
            {
                list.Add(item);
            }
            var collaborator = from t in context.tblCollaborator where t.SharedId == Email select t;

            foreach (var emaildata in collaborator)
            {
                var noteid = emaildata.ID;
                NotesModel note = context.Notes.Where<NotesModel>(t => t.Id == noteid).First();
                list.Add(note);
            }
            data.noteData = list;
            data.label = label;
            return data;
        }
    }
}