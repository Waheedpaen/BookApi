using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services;

public interface IChatServices
{
    Task<MessageReply> SendReply(MessageReply model);
    Task<Message> SendMessage(Message model);
}
