using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooData.Models;

namespace FundooAPI.Services
{
    public class NotesService : INotesService
    {
        private readonly List<NotesModel> _notesModels;
        public NotesService()
        {
            _notesModels = new List<NotesModel>()
           {
               new NotesModel(){Id=new Guid("502a6059-9ee2-4d24-cc44-08d6b41076fb"),Title="First Notes"
               }
           }; 
        }
        public NotesModel Add(NotesModel newItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotesModel> GetAllItems()
        {
            return _notesModels;
        }

        public NotesModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
