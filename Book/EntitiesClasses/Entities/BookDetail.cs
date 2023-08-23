 

namespace EntitiesClasses.Entities;
 
    public  class BookDetail :CommonClass
    {
    public string Image { get; set; }
    [ForeignKey("Scholar")]
    public int ScholarId { get; set; }
    public virtual Scholar  Scholar { get; set; }

}
 