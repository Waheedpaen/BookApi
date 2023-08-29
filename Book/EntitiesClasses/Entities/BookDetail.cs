﻿ 

namespace EntitiesClasses.Entities;
 
    public  class BookDetail :CommonClass
    {
    public BookDetail()
    {
        this.BookImages = new List<BookImage>();
    }
    public string ImageUrl { get; set; }
    [ForeignKey("Scholar")]
    public int ScholarId { get; set; }
    public virtual Scholar  Scholar { get; set; }
    public virtual ICollection<BookImage>  BookImages { get; set; }

}
 