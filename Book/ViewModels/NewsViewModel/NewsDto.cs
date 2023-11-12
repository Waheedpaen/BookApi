using EntitiesClasses.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.NewsViewModel;

public class NewsDto : Entity
{
 
    public string? Header { get; set; } 
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
