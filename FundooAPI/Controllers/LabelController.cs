using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FundooAPI.DataContext;
using FundooAPI.Models;
using FundooData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly AuthenticationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LabelController(AuthenticationContext authenticationContext, UserManager<ApplicationUser> userManager)
        {
            _context = authenticationContext;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("AddLabel")]
        public int AddLabel([FromBody]LableTbl label)
        {


            var labelData = new LableTbl
            {
                Email = label.Email,
                Label = label.Label
            };
            int result = 0;
            try
            {
                _context.tblLabel.Add(labelData);
                result = _context.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return result;
        }

        // to get the label
        [HttpGet("{Email}")]
        public List<LableTbl> GetLabel([FromRoute]string Email)
        {
            var list = new List<LableTbl>();
            var Labeldata = from t in _context.tblLabel where t.Email == Email select t;
            try
            {
                foreach (var data in Labeldata)
                {
                    list.Add(data);
                }
                return list;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return list;
        }

        // to update th label content
        [HttpPut("{ID}")]
        // [Route("update")]
        public void UpdateLabel([FromRoute]int ID, [FromBody]LableTbl label)
        {
            LableTbl note = _context.tblLabel.Where<LableTbl>(t => t.ID == ID).First();
            note.Label = label.Label;
            int result = 0;
            try
            {
                result = _context.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //to delete the label for that particular ID
        [HttpDelete("{ID}")]
        public void DeleteLabel([FromRoute]int ID)
        {
            var label = _context.tblLabel.Where<LableTbl>(t => t.ID == ID).First();
            int result = 0;
            try
            {
                _context.tblLabel.Remove(label);
                result = _context.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}