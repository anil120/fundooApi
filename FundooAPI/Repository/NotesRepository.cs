using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooAPI.DataContext;
using FundooData.Models;
using Microsoft.AspNetCore.Mvc;

namespace FundooAPI.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly AuthenticationContext context;

        public NotesRepository(AuthenticationContext context)
        {
            this.context = context;
        }
        public  bool AddNotes([FromBody] NotesModel notesModel)
        {

            context.Notes.Add(notesModel);
             context.SaveChangesAsync();

            return true;
        }

        public Task<IActionResult> DeleteNotes([FromRoute] Guid id)
        { 
            throw new NotImplementedException();
        }

        public ICollection<NotesModel> GetNotes()
        {
            return context.Notes.ToList();
        }

        public Task<IActionResult> PutNotes([FromRoute] Guid id, [FromBody] NotesModel notesModel)
        {
            throw new NotImplementedException();
        }
    }
}
