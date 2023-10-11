using System;
using System.Collections.Generic;

    namespace ViewModels.FarqaCategoryViewModel;

    public class FarqaCategoryDto
    {
    public int Id { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }
    public string? ImageUrl { get; set; }

    public int BookCategoryId { get; set; }

}

