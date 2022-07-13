using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel.DtoIn
{
    public class ApplicationUserInDto
    {
        [MaxLength(255)]
        public string? UserName { get; set; }
        [MaxLength(255)]
        public string? Password { get; set; }

        [MaxLength(50)]
        public string? Race { get; set; }
        [MaxLength(50)]
        public string? Home { get; set; }
        [MaxLength(50)]
        public string? Occupation { get; set; }
        [MaxLength(50)]
        public string? Experience { get; set; }
        public bool? hasEquipment { get; set; }
        [MaxLength(4095)]
        public string? AboutMe { get; set; }
        [MaxLength(100)]
        public string? Likes { get; set; }
        [MaxLength(100)]
        public string? Dislikes { get; set; }
        [MaxLength(50)]
        public string? Specialty { get; set; }
        public bool? isListed { get; set; }

    }
}
