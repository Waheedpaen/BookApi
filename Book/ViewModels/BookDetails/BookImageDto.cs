

using EntitiesClasses.CommonClasses;

namespace ViewModels.BookDetails;
 
    public  class BookImageDto : CommonClass
    {
        public string Image { get; set; }

        public string? FileNamePDF { get; set; }
        public string? FilePathPDF { get; set; }
        public int BookDetailId { get; set; }
    }
 
