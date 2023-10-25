using HelperData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory
{
    public class ChatRepository : IChatRepository
    {
        protected readonly DbContext Context;
        public ChatRepository(DbContext context)
        {
            Context = context;

        }

        public async Task<Message> SendMessage(Message model)
        {
            await Context.Set<Message>().AddAsync(model);
            return model;
        
        }

        public async Task<MessageReply> SendReply(MessageReply model)
        {
            
          await   Context.Set<MessageReply>().AddAsync(model);         
            return model;
        }


    }
}
