using FundooData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Services
{
    public interface INotesService
    {
        IEnumerable<NotesModel> GetAllItems();
        NotesModel Add(NotesModel newItem);
        NotesModel GetById(Guid id);
        void Remove(Guid id);
    }
}
