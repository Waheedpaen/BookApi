using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.AudioDetailViewModel;

public  class AudioDetailSaveViewModel
{
    public int? Id { get; set; }
    public IFormFile? ImageUrlData { get; set; }
 
    public string? Name { get; set; }
    public DateTime? DateRelase { get; set; } 
    public int? AudioScholarsId { get; set; } 
}
