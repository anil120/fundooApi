using FundooData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Repository
{
    public interface INotesRepository
    {
        bool AddNotes([FromBody] NotesModel notesModel);
        Task<IActionResult> PutNotes([FromRoute] Guid id, [FromBody] NotesModel notesModel);
        Task<IActionResult> DeleteNotes([FromRoute] Guid id);
        ICollection<NotesModel> GetNotes();
    }
}
