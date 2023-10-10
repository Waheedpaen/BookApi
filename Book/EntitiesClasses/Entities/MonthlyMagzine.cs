using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities;

public  class MonthlyMagzine : CommonClass
{
    public string? ImageUrl { get; set; }
    public string? FileNamePDF { get; set; }
    public string? FilePathPDF { get; set; }
    public string? Description { get; set; }
}
