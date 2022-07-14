using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context) { }

        public async Task<Message?> GetMessageAsync(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<Message> GetAllMessagesAsync()
        {
            return _context.Messages;
        }
        public async Task<bool> SaveMessageAsync(Message Message)
        {
            if (Message == null)
            {
                return false;
            }

            try
            {
                _context.Entry(Message).State = Message.Id == default(int) ? EntityState.Added : EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteMessageAsync(Message Message)
        {
            if (Message == null)
            {
                return false;
            }

            try
            {
                _context.Messages.Remove(Message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
