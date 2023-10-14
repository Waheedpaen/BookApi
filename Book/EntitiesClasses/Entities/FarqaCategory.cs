 

namespace EntitiesClasses.Entities;
 
    public  class FarqaCategory : CommonClass
{
  

    [ForeignKey("BookCategory")]
    public int  ?  BookCategoryId { get; set; }
    public string? ImageUrl { get; set; }

    public virtual BookCategory ?  BookCategory { get; set; }
    }
 
