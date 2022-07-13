using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool isListed { get; set; }
        public ApplicationUserDetails Details { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> SendMessages { get; set; }
    }
}
