using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.CommonClasses;

public  class Entity
{
    [Key]
    public int   Id { get; set; }
    public DateTime? Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; } = DateTime.Now;
    public Boolean IsDeleted { get; set; }
}
