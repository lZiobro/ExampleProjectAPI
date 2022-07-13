using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public string? SenderId { get; set; }
        public string SenderName { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public string? ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime DateSend { get; set; }
        public bool Read { get; set; }
    }
}
