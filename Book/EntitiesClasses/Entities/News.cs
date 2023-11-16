namespace EntitiesClasses.Entities;

public  class News : Entity
{

    [Column(TypeName = "nvarchar(max)")] 
    public string   Header { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string  Title { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string   Description { get; set; }
    public string ImageUrl { get; set; }
}
