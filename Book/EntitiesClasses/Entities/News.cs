namespace EntitiesClasses.Entities;

public  class News : Entity
{

    [Column(TypeName = "varchar(MAX)")] 
    public string  ? Header { get; set; }
    [Column(TypeName = "varchar(MAX)")]
    public string ? Title { get; set; }
    [Column(TypeName = "varchar(MAX)")]
    public string ?  Description { get; set; }
    public string? ImageUrl { get; set; }
}
