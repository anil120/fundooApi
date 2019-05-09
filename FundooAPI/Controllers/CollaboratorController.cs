using System;
using System.Collections.Generic;
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
    public class CollaboratorController : ControllerBase
    {
        private readonly AuthenticationContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CollaboratorController(AuthenticationContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("ShareCollaborator")]
        public async Task<object> ShareCollaborator([FromBody] CollaboratorViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Collaborator);
            if(user.Email == model.Collaborator)
            {
                return user;
            }
            return null;
        }

        [HttpPost]
        [Route("AddNotesCollaborator")]
        public object AddNotesCollaborator([FromBody]CollaboratorTbl tbl)
        {  
            var labeldata = from t in context.tblCollaborator where t.Email == tbl.Email select t;
            foreach(var label in labeldata.ToList())
            {
                if(label.ID==tbl.ID && label.SharedId == tbl.SharedId)
                {
                    return BadRequest();
                }
            }
            var data = new CollaboratorTbl
            {
                ID=tbl.ID,
                NoteId=tbl.NoteId,
                Email=tbl.Email,
                SharedId=tbl.SharedId
            };
            int result = 0;
            context.tblCollaborator.Add(data);
            result = context.SaveChanges();
            return Ok();
        }

        [HttpGet("{Email}")]
        public IList<CollaboratorTbl> GetCollaborators(string Email)
        {
            var list = new List<CollaboratorTbl>();
            var label = from t in context.tblCollaborator where t.Email == Email select t;
            try
            {
                foreach (var data in label)
                {
                    list.Add(data);
                }
            }catch(Exception e)
            {
                e.ToString();
            }
            return list;
        }

        [HttpDelete("{ID}")]
        public void DeleteCollaborator(Guid id)
        {
            var label = context.tblCollaborator.Where<CollaboratorTbl>(t => t.ID == id).First();
            int result = 0;
            try
            {
                context.tblCollaborator.Remove(label);
                result = context.SaveChanges();
            }catch(Exception e)
            {
                e.ToString();
            }
        }
    }
}