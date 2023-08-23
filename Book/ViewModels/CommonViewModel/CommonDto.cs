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
        public int   Id  { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
       
    }
}
