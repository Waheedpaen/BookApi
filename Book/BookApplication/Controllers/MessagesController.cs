using CoreWebApi.Hubs;
using HelperData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ViewModels.CommonViewModel;
using static ViewModels.MessageChatViewModel.MessageDto;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    //private readonly IMessageRepository _repo;
    private readonly IMapper _mapper;
    private readonly IChatServices _chatServices;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHubContext<ChatHub> _hubContext;
    private int _LoggedIn_UserID = 0;
    //private readonly IFilesRepository _filesRepository;
    //private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
    public MessagesController(IConfiguration config, IChatServices 
        chatServices, IMapper mapper, IHubContext<ChatHub> hubContext, IHttpContextAccessor httpContextAccessor)
    {
        _config = config; 
      _chatServices = chatServices;
       _hubContext = hubContext; 
        
    
    }

    [HttpPost("SendReply")]
    public async Task<IActionResult> SendReply([FromForm] ReplyForAddDto model)
    {
        var messageReply = new MessageReply
        {
            Reply = model.Reply,
            ReplyToUserId = model.ReplyToUserId,
            IsRead = false,
            CreatedDateTime = DateTime.UtcNow,
            ReplyFromUserId = model._LoggedIn_UserID,
            MessageId = Convert.ToInt32(model.MessageId),
        };

           await _chatServices.SendReply(messageReply);
            return Ok(new { Success = true, Message = CustomMessage.Added });
      
    }

}
