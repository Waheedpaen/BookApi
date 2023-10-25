using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Services;

public class ChatServices : IChatServices
{
    private readonly IUnitofWork _unitOfWork;

    public ChatServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<Message> SendMessage(Message model)
    {
        await _unitOfWork.IChatRepository.SendMessage(model);
        await _unitOfWork.CommitAsync();
        return model;  
    }

    public async Task<MessageReply> SendReply(MessageReply model)
    {
        await _unitOfWork.IChatRepository.SendReply(model);
        await _unitOfWork.CommitAsync();
        return model;
    }
}
