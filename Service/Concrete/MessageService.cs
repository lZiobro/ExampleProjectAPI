﻿using AutoMapper;
using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            this._messageRepository = messageRepository;
            this._mapper = mapper;
        }

        public async Task<bool> AddNewMessageAsync(MessageInDto messageInDto)
        {
            var newMessage = _mapper.Map<Message>(messageInDto);
            newMessage.DateSend = DateTime.Now;
            newMessage.Read = false;
            bool result = await _messageRepository.SaveMessageAsync(newMessage);
            return result;
        }

        public async Task<List<MessageOutDto>> GetAllUserMessagesOutAsync(string userId)
        {
            var messages = await _messageRepository.GetAllMessagesAsync();
            var userMessages = messages.Where(x => x.SenderId == userId);
            var mappedMessages = _mapper.Map<List<MessageOutDto>>(userMessages);
            return mappedMessages;
        }
        public async Task<List<MessageOutDto>> GetAllUserMessagesInAsync(string userId)
        {
            var messages = await _messageRepository.GetAllMessagesAsync();
            var userMessages = messages.Where(x => x.ReceiverId == userId);
            var mappedMessages = _mapper.Map<List<MessageOutDto>>(userMessages);
            return mappedMessages;
        }

        public async Task<MessageOutDto?> GetMessageAsync(int id)
        {
            var message = await _messageRepository.GetMessageAsync(id);
            var mappedMessage = _mapper.Map<MessageOutDto>(message);
            return mappedMessage;
        }
    }
}
