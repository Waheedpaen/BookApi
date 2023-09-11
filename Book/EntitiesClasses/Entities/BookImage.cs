 

namespace EntitiesClasses.Entities;

public class BookImage  : CommonClass
{
    public string Image { get; set; }

    public string? FileNamePDF { get; set; }
    public string? FilePathPDF { get; set; }
    [ForeignKey("BookDetail")]
    public int ? BookDetailId { get; set; }
    public virtual BookDetail ? BookDetail { get; set; }


}
