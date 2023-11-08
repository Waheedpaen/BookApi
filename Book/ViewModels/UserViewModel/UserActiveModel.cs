using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.UserViewModel;

public class UserActiveModel
{ 
    public int   Id { get; set; }
    public int UserTypesId { get; set; }
    public Boolean IsDeleted { get; set; }
}
 
