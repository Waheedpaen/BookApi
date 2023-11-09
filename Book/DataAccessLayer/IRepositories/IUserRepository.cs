


using EntitiesClasses.Entities;
using HelperData;
using ViewModel.ViewModels.UserViewModel;
using ViewModels.UserViewModel;

namespace DataAccessLayer.IRepositories;

public interface IUserRepository : IRepository<User, int>
{
    Task<User> Logout(int loginUserId);
    Task<LoginUserDto> Login(UserDtoLogin model); 
    Task<ServiceResponse<object>> AddUser(UserAddDto model);
     
    Task<bool> UserAlreadyExit(string Name);
    Task<bool> UserNameAlreadyExit(string Name);
     Task<bool> ActiveOrDeactiveUser(UserActiveModel model);
    Task<User> UserEmailAlreadyExitForVerify(string emailAddress);

    Task<bool> UserEmailAlreadyExit(string Name);

    Task<List<User>> GetActiveUsers();
    Task<List<User>> GetDeActiveUsers(); 
    Task<List<UserTypes>> GetUserTypes();

    Task<int> GetUserCount();
    Task<User> GetUser(int userId);
    Task<List<User>> SearchingData(string name);
    Task<EmailVerificationCode> verifyEmailCodeAndEmail(EmailVerificationCode model);

    Task<bool> UserPhoneAlreadyExit(string Name);
    Task<EmailVerificationCode> verifyEmailCodeAndEmailCheck(string emailAddress);
     Task<bool> UserHaveDeleted(string Email); 
     Task<bool> AssignRoleToUser(AssignRoleToUserModel model);


}
   
