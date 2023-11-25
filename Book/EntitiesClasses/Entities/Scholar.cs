
    namespace EntitiesClasses.Entities;

    public  class Scholar : CommonClass
    {
    [Column(TypeName = "varchar(900)")]
    public string MadrassaName { get; set; }
        public string? ImageUrl { get; set; }
    [ForeignKey("FarqaCategory")]
    public int FarqaCategoryId { get; set; } 
    public virtual FarqaCategory  FarqaCategory { get; set; }
}

