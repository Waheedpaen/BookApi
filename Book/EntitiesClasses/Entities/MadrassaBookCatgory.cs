using System;
 
namespace EntitiesClasses.Entities;
 
   public  class MadrassaBookCatgory : CommonClass
    {
    public string ? ImageUrl { get; set; }
    public string? FileNamePDF { get; set; }
    public string? FilePathPDF { get; set; }
    [ForeignKey("MadrassaBook")]
    public int ? MadrassaBookId { get; set; }
    public virtual  MadrassaBook ? MadrassaBook { get; set; }
}
 
