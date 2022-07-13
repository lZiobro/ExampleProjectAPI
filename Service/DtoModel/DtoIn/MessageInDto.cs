using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel
{
    public class MessageInDto
    {
        [MaxLength(450)]
        public string? SenderId { get; set; }
        [MaxLength(255)]
        public string? SenderName { get; set; }
        [MaxLength(450)]
        public string? ReceiverId { get; set; }
        [MaxLength(255)]
        public string? ReceiverName { get; set; }
        [MaxLength(255)]
        [Required()]
        public string Topic { get; set; }
        [MaxLength(4095)]
        [Required()]
        public string Content { get; set; }
    }
}
