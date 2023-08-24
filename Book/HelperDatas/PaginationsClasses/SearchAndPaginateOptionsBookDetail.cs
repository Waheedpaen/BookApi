﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperDatas.PaginationsClasses
{
    public class SearchAndPaginateOptionsBookDetail
    {
        public string SearchTerm { get; set; }
        public string Age { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
