

using EntitiesClasses.Entities;
using HelperData;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using ViewModel.ViewModels.UserViewModel;
using ViewModels.UserViewModel;

namespace DataAccessLayer.Services;
 
     public  interface IUserService
     {
    Task<bool> ActiveOrDeactiveUser(UserActiveModel model);
    Task<User> Logout(int loginUserId);
    Task<LoginUserDto> Login(UserDtoLogin model);

    Task<ServiceResponse<object>> AddUser(UserAddDto model);
    Task<bool> UserAlreadyExit(string Name);
    Task<bool> UserNameAlreadyExit(string Name);
    Task<User> ChangePassword(User update, ChangePasswordDto model);
    Task<List<User>> ActiveUsers();
  Task<List<User>> GetDeActiveUsers();

    Task<int> GetUserCount();
    Task<bool> CheckUserNameExistence(string Name);
    Task<bool> UserEmailAlreadyExit(string Name);
    Task<EmailVerificationCode> VerifyEmailCodeAndEmail(EmailVerificationCode model);

    Task<bool> UserPhoneAlreadyExit(string Name); 
    Task<User> UpdateUser(User update, User model);
    Task<User> GetUser(int userId);
    Task<List<User>> SearchingData(string name);
    Task<User> Delete(User model);
    Task<List<UserTypes>> GetUserTypes();
    Task<User> UserEmailAlreadyExitForVerify(string emailAddress);
    Task<EmailVerificationCode> verifyEmailCodeAndEmailCheck(string emailAddress);

    Task<PagedResult<User>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
    Task<bool> UserHaveDeleted(string Email);
    Task<bool> AssignRoleToUser(AssignRoleToUserModel model);
}
 
