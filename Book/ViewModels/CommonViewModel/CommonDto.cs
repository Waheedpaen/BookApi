using EntitiesClasses.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CommonViewModel
{
    public class CommonDto
    {
        public int  ? Id  { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime? Created_At { get; set; }  
        public DateTime? Updated_At { get; set; }  

    }
}
