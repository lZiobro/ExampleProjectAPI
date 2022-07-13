using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DtoModel
{
    public class MessageOutDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Topic { get; set; }
        public string Content { get; set; }
        public DateTime DateSend { get; set; }
    }
}
