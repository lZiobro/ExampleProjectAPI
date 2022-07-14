using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IMessageRepository
    {
        Task<Message?> GetMessageAsync(int id);
        IQueryable<Message> GetAllMessagesAsync();
        Task<bool> SaveMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(Message message);
    }
}
