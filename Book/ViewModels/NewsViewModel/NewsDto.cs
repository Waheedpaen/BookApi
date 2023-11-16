using EntitiesClasses.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.NewsViewModel;

public class NewsDto : Entity
{

    [Column(TypeName = "nvarchar(max)")]
    public string Header { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Title { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Description { get; set; }
    public string ImageUrl { get; set; }

}
