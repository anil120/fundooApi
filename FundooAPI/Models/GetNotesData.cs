
namespace FundooAPI.Models
{
    using FundooData.Models;
    using System.Collections.Generic;
    public class GetNotesData
    {
        public List<NotesModel> noteData { get; set; }
        public List<LableTbl> label { get; set; }
        public List<CollaboratorTbl> collaborator { get; set; }
    }
}
