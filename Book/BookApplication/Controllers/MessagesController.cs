using Azure;
using CoreWebApi.Hubs;
using EntitiesClasses.Entities;
using HelperData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.Security.Claims;
using ViewModels.CommonViewModel;
using ViewModels.MessageChatViewModel;
using static ViewModels.MessageChatViewModel.MessageDto;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    //private readonly IMessageRepository _repo;
    private readonly IMapper _mapper;
    private readonly IChatServices _chatServices;
    private readonly DataContexts _context;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHubContext<ChatHub> _hubContext;
    protected ServiceResponse<object> _serviceResponse;
    private int _LoggedIn_UserIDs = 0;
    //private readonly IFilesRepository _filesRepository;
    //private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
    public MessagesController(IConfiguration config, IChatServices 
        chatServices, IMapper mapper, DataContexts dataContexts,IHubContext<ChatHub> hubContext, IHttpContextAccessor httpContextAccessor)
    {
        _config = config;
        _context = dataContexts;
      _chatServices = chatServices;
       _hubContext = hubContext;
        _serviceResponse = new ServiceResponse<object>();

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
    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromForm] MessageForAddDto model)
    {
        var obj = "20".ToInt32();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        //List<string> ReceiverConnectionids = _connections.GetConnections(model.MessageToUserId.ToString()).ToList<string>();
        var ToAdd = new Message
        {
            Comment = model.Comment,
            MessageToUserId = model.MessageToUserId,
            IsRead = false,
            CreatedDateTime = DateTime.UtcNow,
            MessageFromUserId = model._LoggedIn_UserID,
            MessageReplyId = model.MessageReplyId,
        };
        await _chatServices.SendMessage(ToAdd);
        var userToIds = new List<string>() { model.MessageToUserId.ToString() };
        int UserId = Convert.ToInt32(userToIds.First());
        var forSignal = false;
        List<GroupMessageForListByTimeDto> Messages = new List<GroupMessageForListByTimeDto>();
        if (!forSignal)
        {
            var Users = await _context.Users.Where(m => userToIds.Contains(m.Id.ToString())).ToListAsync();
            var UserToDetails = Users.Count > 0 ? _mapper.Map<List<UserForDetailedDto>>(Users) : new List<UserForDetailedDto>();
            var SentMessages = _context.Messages.Where(m => m.CreatedDateTime.Date >= DateTime.UtcNow.AddDays(-7).Date && ((m.MessageFromUserId == model._LoggedIn_UserID && UserId.Equals(m.MessageToUserId))
            || (UserId.Equals(m.MessageFromUserId) && m.MessageToUserId.Equals(model._LoggedIn_UserID))))
                .Select(o => new GroupMessageForListDto
                {
                    Id = o.Id,
                    Time = HelperData.DateFormat.FromUTCToPKFormat(o.CreatedDateTime),
                    TimeToDisplay = HelperData.DateFormat.ToTime(o.CreatedDateTime.TimeOfDay),
                    MessageFromUserId = Convert.ToInt32(o.MessageFromUserId),
                    MessageFromUser = o.MessageFromUser != null ? o.MessageFromUser.UserName : "",
                    Comment = o.Comment,
                    
                    MessageToUserIdsStr = o.MessageToUserId.ToString(),
                    Type = o.MessageFromUserId == model._LoggedIn_UserID  ? "1" : "2" // 1=Message, 2=Reply
                }).ToList();

            var DateTimes = _context.Messages.Where(m => m.CreatedDateTime.Date >= DateTime.UtcNow.AddDays(-7).Date && ((m.MessageFromUserId == model._LoggedIn_UserID && UserId.Equals(m.MessageToUserId))
            || (UserId.Equals(m.MessageFromUserId) && m.MessageToUserId.Equals(model._LoggedIn_UserID))))
                .OrderBy(m => m.CreatedDateTime)
                .Select(m => HelperData.DateFormat.ToDateTime(m.CreatedDateTime)).ToList();
            DateTimes = DateTimes.Distinct().ToList();
            for (var i = 0; i < DateTimes.Count(); i++)
            {
                var item = DateTimes[i];
                var ToAdds = new GroupMessageForListByTimeDto();
                DateTime dt = Convert.ToDateTime(item, CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                var PKZone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                var Today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, PKZone);
                if (dt.Date == Today.Date)
                    ToAdds.TimeToDisplay = Messages.Any(m => m.TimeToDisplay == "Today") ? dt.ToString("hh:mm tt") : "Today";
                else if (dt.Date == Today.AddDays(-1).Date)
                    ToAdds.TimeToDisplay = Messages.Any(m => m.TimeToDisplay == "Yesterday") ? dt.ToString("hh:mm tt") : "Yesterday";
                else
                    ToAdds.TimeToDisplay = Messages.Any(m => m.TimeToDisplay == item) ? "" : item;
                ToAdds.Messages = SentMessages.Where(m => m.Time.Date == dt.Date && m.Time.Hour == dt.Hour && m.Time.Minute == dt.Minute).OrderBy(m => m.Time).ToList();
                Messages.Add(ToAdds);
            }

            var obj = new { UserToDetails, Messages };
            _serviceResponse.Data = obj;




        }
        else
        {
            var SingleSentMessage = _context.Messages.Where(m => m.CreatedDateTime.Date >= DateTime.UtcNow.AddDays(-7).Date && ((m.MessageFromUserId == model._LoggedIn_UserID && UserId.Equals(m.MessageToUserId))
            || (UserId.Equals(m.MessageFromUserId) && m.MessageToUserId.Equals(model._LoggedIn_UserID))))
                .Select(o => new GroupMessageForListDto
                {
                    Id = o.Id,
                    Time = HelperData.DateFormat.FromUTCToPKFormat(o.CreatedDateTime),
                    TimeToDisplay = HelperData.DateFormat.ToTime(o.CreatedDateTime.TimeOfDay),
                    MessageFromUserId = Convert.ToInt32(o.MessageFromUserId),
                    MessageFromUser = o.MessageFromUser != null ? o.MessageFromUser.UserName : "",
                    Comment = o.Comment,
                
                    MessageToUserIdsStr = o.MessageToUserId.ToString(),
                    Type = o.MessageFromUserId == model._LoggedIn_UserID ? "1" : "2" // 1=Message, 2=Reply
                }).OrderByDescending(m => m.Id).FirstOrDefault();

            var ToAdds = new GroupMessageForListByTimeDto();

            ToAdds.Messages.Add(SingleSentMessage);
            Messages.Add(ToAdds);

            _serviceResponse.Data = Messages.FirstOrDefault();

        }
        if (_serviceResponse.Success)
        {
            var lastMessageStr = JsonConvert.SerializeObject(_serviceResponse.Data);
            var lastMessage = JsonConvert.DeserializeObject<GroupMessageForListByTimeDto>(lastMessageStr);
            var ToReturn = new GroupSignalRMessageForListDto
            {
                Id = lastMessage.Messages[0].Id,
                Type = lastMessage.Messages[0].Type,
                DateTimeToDisplay = lastMessage.Messages[0].TimeToDisplay,
                TimeToDisplay = lastMessage.Messages[0].TimeToDisplay,
                Comment = lastMessage.Messages[0].Comment,
                MessageFromUserId = lastMessage.Messages[0].MessageFromUserId,
                MessageFromUser = lastMessage.Messages[0].MessageFromUser,
                MessageToUserIdsStr = lastMessage.Messages[0].MessageToUserIdsStr,
                GroupId = 0,
                Attachment = lastMessage.Messages[0].Attachment,
                FileName = lastMessage.Messages[0].FileName,
                FileType = lastMessage.Messages[0].FileType,

                //MessageToUser = lastMessage.Messages[0].MessageToUser,
            };

            // List<MessageForListByTimeDto> collection = new List<MessageForListByTimeDto>((IEnumerable<MessageForListByTimeDto>)lastMessage.Data);

            await _hubContext.Clients.All.SendAsync("MessageNotificationAlert", ToReturn);
            //_hubContext.Clients.Clients(ReceiverConnectionids)
        }

        return Ok(_serviceResponse);
        //var response = await _repo.GetChatMessages(userToIds, true);
        //if (_response.Success)
        //{
        //    var lastMessageStr = JsonConvert.SerializeObject(response.Data);
        //    var lastMessage = JsonConvert.DeserializeObject<GroupMessageForListByTimeDto>(lastMessageStr);
        //    var ToReturn = new GroupSignalRMessageForListDto
        //    {
        //        Id = lastMessage.Messages[0].Id,
        //        Type = lastMessage.Messages[0].Type,
        //        DateTimeToDisplay = lastMessage.Messages[0].TimeToDisplay,
        //        TimeToDisplay = lastMessage.Messages[0].TimeToDisplay,
        //        Comment = lastMessage.Messages[0].Comment,
        //        MessageFromUserId = lastMessage.Messages[0].MessageFromUserId,
        //        MessageFromUser = lastMessage.Messages[0].MessageFromUser,
        //        MessageToUserIdsStr = lastMessage.Messages[0].MessageToUserIdsStr,
        //        GroupId = 0,
        //        Attachment = lastMessage.Messages[0].Attachment,
        //        FileName = lastMessage.Messages[0].FileName,
        //        FileType = lastMessage.Messages[0].FileType,

        //        //MessageToUser = lastMessage.Messages[0].MessageToUser,
        //    };

        //    // List<MessageForListByTimeDto> collection = new List<MessageForListByTimeDto>((IEnumerable<MessageForListByTimeDto>)lastMessage.Data);

        //    await _hubContext.Clients.All.SendAsync("MessageNotificationAlert", ToReturn);
        //    //_hubContext.Clients.Clients(ReceiverConnectionids)
        //}
         

    }
}
