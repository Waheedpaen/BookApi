using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities;

public class MessageReply
{
    public int Id { get; set; }

 
    public string Reply { get; set; }
   
    public string Attachment { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public bool IsRead { get; set; }

    [ForeignKey("ReplyFromUser")]
    public int ? ReplyFromUserId { get; set; }
    public virtual User?  ReplyFromUser { get; set; }
    [ForeignKey("ReplyToUser")]
    public int? ReplyToUserId { get; set; }
    public virtual User ? ReplyToUser { get; set; }
    [ForeignKey("MessageId")]
    public int MessageId { get; set; }
    public virtual Message ? Message { get; set; }
}
