using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViewModels.MessageChatViewModel.MessageDto;

namespace DataAccessLayer.IRepositories
{
    public  interface IChatRepository
    { 
      Task<MessageReply> SendReply(MessageReply model); 
      Task<Message> SendMessage(Message model);
    }
}
