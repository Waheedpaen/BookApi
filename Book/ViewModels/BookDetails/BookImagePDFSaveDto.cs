using EntitiesClasses.CommonClasses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BookDetails
{
    public class BookImagePDFSaveDto 
    {
        public IFormFile? PdfFile { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int BookDetailId { get; set; }
    }
}
