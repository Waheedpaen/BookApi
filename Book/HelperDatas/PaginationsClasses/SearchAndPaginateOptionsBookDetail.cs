using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperDatas.PaginationsClasses
{
    public class SearchAndPaginateOptions 
    {
        public string ? SearchTerm { get; set; } 
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string ? MadrassaName { get; set; }
        public string? Name { get; set; }
        public string ? ScholarName{ get; set; }
    }
}
