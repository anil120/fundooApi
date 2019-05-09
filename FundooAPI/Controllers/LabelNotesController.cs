using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooAPI.DataContext;
using FundooData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelNotesController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public LabelNotesController(AuthenticationContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("AddNotesLabel")]
        public bool AddNotesLabel([FromBody]NotesLabelTable model)
        {
            var Labeldata = from t in _context.tblNotesLabel where t.Email == model.Email select t;

            foreach (var data1 in Labeldata.ToList())
            {
                if (data1.NoteId == model.NoteId && data1.LableId == model.LableId)
                {
                    return false;
                }
            }
            var data = new NotesLabelTable
            {
                Email = model.Email,
                LableId = model.LableId,
                NoteId = model.NoteId
            };
            int result = 0;

            _context.tblNotesLabel.Add(data);
            result = _context.SaveChanges();
            return true;
        }

        [HttpGet("{Email}")]
        [Route("getNotesLabel")]
        public List<NotesLabelTable> GetNotesLabel([FromRoute]string Email)
        {
            var list = new List<NotesLabelTable>();
            var Labeldata = from t in _context.tblNotesLabel where t.Email == Email select t;
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
        [HttpDelete("{ID}")]
        [Route("deleteNotesLabel")]
        public void DeleteNotesLabel(int ID)
        {
            var label = _context.tblNotesLabel.Where<NotesLabelTable>(t => t.ID == ID).First();
            int result = 0;
            try
            {
                _context.tblNotesLabel.Remove(label);
                result = _context.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

    }
}