using EntitiesClasses.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ScholarViewModel;

namespace ViewModels.BookDetails
{
    public class BookDetailDto : CommonClass
    {
        public BookDetailDto()
        {
            this.BookImages = new List<BookImageDto>();
        }
        public string ImageUrl { get; set; }

        public int ScholarId { get; set; }
        public virtual ScholarDto Scholar { get; set; }
        public virtual List<BookImageDto> BookImages { get; set; }
    }
}
