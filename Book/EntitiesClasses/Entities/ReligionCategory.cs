 

namespace EntitiesClasses.Entities;
 
    public  class ReligionCategory : CommonClass
{
  

    [ForeignKey("BookCategory")]
    public int BookCategoryId { get; set; }
    public virtual BookCategory BookCategory { get; set; }
    }
 
