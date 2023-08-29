 

namespace ViewModels.MadrassaBookViewModel;

public class MadrassaBookCatgorySaveDto
{

    public int? Id { get; set; }
    public  IFormFile? PdfFile { get; set; }
    public string? ImageUrl { get; set; }
    public string Name { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsSaved { get; set; }
    public int MadrassaBookId { get; set; }
  
}
