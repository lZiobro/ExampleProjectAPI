using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IMessageService
    {
        Task<bool> AddNewMessageAsync(MessageInDto messageInDto);
        Task<MessageOutDto?> GetMessageAsync(int id);
        Task<List<MessageOutDto>> GetAllUserMessagesOutAsync(string userId);
        Task<List<MessageOutDto>> GetAllUserMessagesInAsync(string userId);
        //NO DELETING! (for now at least...)
        //Task<bool> DeleteMessageAsync(int id);
    }
}
