using ApiGateway.Dto_Models;
using ApiGateway.Services.ChatService;
using ChatGRPCService.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        // To Get All Conversation of the Current user. 
        [HttpGet("get/conversations"), Authorize]
        public GetConversationsResponse GetConversations()
        {
            var response = _chatService.GetConversations();

            return response;
        }

        // To Get the Tickets
        [HttpGet("user/tickets"), Authorize]
        public GetTicketsResponse GetTicketsForUser()
        {
            var response = _chatService.GetTicketsForUser();
            
            return response;
        }

        // To Get all the Ticket for Admin
        [HttpGet("all/tickets")]
        public GetTicketsResponse GetTicketsForAdmin()
        {
            var response = _chatService.GetTicketsForAdmin();

            return response;
        }

        // To Create a Ticket
        [HttpPost("ticket"), Authorize]
        public async Task<IActionResult> CreateTicket(CreateTicket queryRequest)
        {
            var response = _chatService.CreateQuery(queryRequest);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }



        // To Close The Ticket
        [HttpDelete("close/Ticket/{ticketId}")]
        public async Task<IActionResult> CloseTicket(int ticketId)
        {

            var response = _chatService.CloseTicket(new ChatPackage.CommonRequest
            {
                RequestId = ticketId
            });

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

        // GET Chats based on the conversationId
        /*[HttpGet("get/chats/{id}"), Authorize]
        public IEnumerable<TimelineCommentModel> GetChats(int id)
        {
            var response = _chatService.GetChats(id);

            return response;
        }*/

        // To GET chats based on the ticketId
        /*[HttpGet("get/chat/tickets/{ticketId}"), Authorize]
        public IEnumerable<TimelineCommentModel> GetChatsForTicket(int ticketId)
        {
            var response = this._chatService.GetChatForTickets(ticketId);

            return response;
        }*/

        // Post a message on the Conversation
        /*[HttpPost("send/message"), Authorize]
        public IActionResult SendMessage([FromBody] ChatDto chat)
        {
            var response = _chatService.AddChat(chat);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }

        // Post a message on the Ticket

        [HttpPost("send/ticket/message"), Authorize]
        public IActionResult SendMessageForTicket([FromBody] ChatTicketDto chat)
        {
            var response = _chatService.AddChatForTicket(chat);

            if (response is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(response);
        }*/
    }
}
