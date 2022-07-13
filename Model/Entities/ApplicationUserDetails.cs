using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ApplicationUserDetails
    {
        public string UserId { get; set; }
        public string? Race { get; set; }
        public string? Occupation { get; set; }
        public string? Experience { get; set; }
        public string? Home { get; set; }
        public bool HasEquipment { get; set; }
        public string? Likes { get; set; }
        public string? Dislikes { get; set; }
        public string? Specialty { get; set; }
        public string? AboutMe { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
