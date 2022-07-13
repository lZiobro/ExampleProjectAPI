using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Service.Abstract;
using Service.DtoModel;

namespace ExampleProjectAPI.Controllers
{
    [Route("api/messages")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    //[Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<ApplicationUser> _userManager;
        public MessagesController(IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }
        [EnableCors("CorsPolicy")]
        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> NewMessageAsync(MessageInDto messageInDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //should never happen, but just in case
            if (user == null) return BadRequest("Not logged in");
            var userId = user.Id;
            var userName = User.Identity.Name;

            messageInDto.SenderId = userId;
            messageInDto.SenderName = userName;

            var receiver = await _userManager.FindByNameAsync(messageInDto.ReceiverName);
            if (receiver == null) return BadRequest("Cant find recipent");
            var receiverId = receiver.Id;

            messageInDto.ReceiverId = receiverId;
            if (messageInDto.SenderId == messageInDto.ReceiverId) return BadRequest("Cant send message to yourself");


            var result = await _messageService.AddNewMessageAsync(messageInDto);
            return result ? Ok("Message Sent") : BadRequest();
        }

        [EnableCors("CorsPolicy")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageAsync(int id)
        {
            var message = await _messageService.GetMessageAsync(id);
            if (message == null) return NotFound();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            if (message.SenderId != user.Id && message.ReceiverId != user.Id) return Unauthorized();

            return Ok(message);
        }

        [EnableCors("CorsPolicy")]
        [Authorize]
        [HttpGet("getAllUserMessagesOut")]
        public async Task<IActionResult> GetAllUserMessagesOutAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //should never happen, but just in case
            if (user == null) return Unauthorized("Not logged in");
            var userId = user.Id;
            var result = await _messageService.GetAllUserMessagesOutAsync(userId);
            if(!result.Any()) return NotFound("No messages found");
            return Ok(result);
        }
        [EnableCors("CorsPolicy")]
        [Authorize]
        [HttpGet("getAllUserMessagesIn")]
        public async Task<IActionResult> GetAllUserMessagesInAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return Unauthorized("Not logged in");
            var userId = user.Id;
            var result = await _messageService.GetAllUserMessagesInAsync(userId);
            if (!result.Any()) return NotFound("No messages found");
            return Ok(result);
        }
    }
}
