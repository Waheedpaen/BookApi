

using EntitiesClasses.CommonClasses;
namespace ViewModel.ViewModels.UserViewModel;
 
    public class UserListDto :CommonClass
    {
    [Required]
    
    public string UserName { get; set; }
    public Boolean ?IsOnlined { get; set; }

    public Boolean ? Offline { get; set; }
    public string Email { get; set; }
    public DateTime? LoginTime { get; set; }

    public DateTime? LastLogout { get; set; }
    //public virtual UserTypesDto UserTypes { get; set; }

    //[ForeignKey("Shop")]
    //public int ShopId { get;set; }
    //public virtual Shop Shop { get; set; }

    public int updatebyId { get; set; }
    public int ?  UserTypeId { get; set; }

    public string ImageUrl { get; set; }
}
 
