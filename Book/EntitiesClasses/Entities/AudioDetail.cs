using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities;

public class AudioDetail : CommonClass
{
    public string? FileNameAudio { get; set; }
    public string? FilePathAudio { get; set; }
    public DateTime? DateRelase { get; set; }
    [ForeignKey("AudioScholars")]
    public int   AudioScholarsId { get; set; }
    public virtual AudioScholars ?AudioScholars { get; set; }

}
