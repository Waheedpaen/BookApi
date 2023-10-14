using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.AudioDetailViewModel
{
    public class AudioDetailDto
    {
        public int ? Id { get; set; } 
        public string Name { get; set; }
        public string? FileNameAudio { get; set; }
        public string? FilePathAudio { get; set; }
        public DateTime? DateRelase { get; set; } 
        public int? AudioScholarsId { get; set; }
    }
}
