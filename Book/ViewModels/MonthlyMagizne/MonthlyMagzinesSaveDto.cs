using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.MonthlyMagizne;

public class MonthlyMagzinesSaveDto
{
    public int? Id { get; set; }
    public IFormFile? PdfFile { get; set; }
    public string ImageUrl { get; set; }
    public string? Name { get; set; }
    
    public string? Description { get; set; }


}
