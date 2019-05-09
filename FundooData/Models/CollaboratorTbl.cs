using System;
using System.Collections.Generic;
using System.Text;

namespace FundooData.Models
{
    public class CollaboratorTbl
    {
        public Guid ID { get; set; }
        public Guid NoteId { get; set; }
        public string Email { get; set; }
        public string SharedId { get; set; }
    }
}
