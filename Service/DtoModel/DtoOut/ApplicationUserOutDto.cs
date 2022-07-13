using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel.DtoOut
{
    public class ApplicationUserOutDto
    {
        public string UserName { get; set; }

        public string? Race { get; set; }
        public string? Occupation { get; set; }
        public string? Experience { get; set; }
        public string? Home { get; set; }
        public bool? HasEquipment { get; set; }
        public string? Likes { get; set; }
        public string? Dislikes { get; set; }
        public string? Specialty { get; set; }
        public string? AboutMe { get; set; }
        public bool? isListed { get; set; }

    }
}
