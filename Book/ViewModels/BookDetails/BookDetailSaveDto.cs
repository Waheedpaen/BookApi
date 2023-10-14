using EntitiesClasses.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ScholarViewModel;

namespace ViewModels.BookDetails
{
    public class BookDetailSaveDto  
    {
        public BookDetailSaveDto()
        {
            this.BookImages = new List<BookImagePDFSaveDto>();
        }
        public int ? Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageUrlData { get; set; }
        public string ? ImageUrl { get; set; }

        public int ScholarId { get; set; }
       
        public virtual List<BookImagePDFSaveDto> BookImages { get; set; }
    }
}
