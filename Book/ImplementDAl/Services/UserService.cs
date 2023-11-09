



using DataAccessLayer.Seeds;
using HelperData;
using ViewModel.ViewModels.UserViewModel;
using ViewModels.UserViewModel;

namespace ImplementDAL.Services;
    public class UserService : IUserService
    {
    private readonly IUnitofWork _unitOfWork;
 
    public UserService(IUnitofWork unitOfWork )
    {
      
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<User>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<User, bool>> predicate = category =>
        string.IsNullOrEmpty(options.SearchTerm) ||
        category.Name.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.IUserRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }


    public async Task<ServiceResponse<object>> AddUser(UserAddDto model)
    { 
   var data =      await _unitOfWork.IUserRepository.AddUser(model);
        await _unitOfWork.CommitAsync();
        return data;
    }

    public async Task<User> ChangePassword(User update, ChangePasswordDto model)
    {
        byte[] passwordHash, passwordSalt;
        Seed.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
        update.PasswordHash = passwordHash;
        update.PasswordSalt = passwordSalt;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<bool> CheckUserNameExistence(string Name)
    {
        var data = await _unitOfWork.IUserRepository.SingleOrDefaultAsync(e => e.Name == Name.Replace("'", "''"));

        if (data == null)
            return false;
        else
            return true;
    }

    public async Task<User> Delete(User model)
    {
         
            model.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return model;
        
    }

    public async Task<User> GetUser(int userId)
    {
        return await _unitOfWork.IUserRepository.GetUser(userId);
    }

    public async Task<int> GetUserCount()
    {
       return await _unitOfWork.IUserRepository.GetUserCount();
    }
    public async Task<List<User>> ActiveUsers()
    {
        return await _unitOfWork.IUserRepository.GetActiveUsers();
    }
    public async Task<List<User>> GetDeActiveUsers()
    {
        return await _unitOfWork.IUserRepository.GetDeActiveUsers();
    }

    public async Task<List<UserTypes>> GetUserTypes()
    {
         return await _unitOfWork.IUserRepository.GetUserTypes();
    }

    public async Task<LoginUserDto> Login(UserDtoLogin model)
    {
     
      var objUser =      await _unitOfWork.IUserRepository.Login(model);
       return objUser;

    }
    public async Task<User> Logout(int loginUserId)
    {
        var userData = await _unitOfWork.IUserRepository.Logout(loginUserId);
        await _unitOfWork.CommitAsync();
        return userData;
    }

    public Task<List<User>> SearchingData(string name)
    {
        return _unitOfWork.IUserRepository.SearchingData(name);
    }

    public async Task<User> UpdateUser(User update, User model)
    {
        try
        {
            update.Name = model.Name;
            update.UserName = model.UserName;
            update.ImageUrl = model.ImageUrl;
            await _unitOfWork.CommitAsync();
            return update;
        }
catch (Exception ex)
        {
            throw (ex);
        }

    }

    public async Task<bool> UserAlreadyExit(string Name)
    {
        return await _unitOfWork.IUserRepository.UserAlreadyExit(Name);
    }
    public async Task<bool> UserEmailAlreadyExit(string Name)
    {
       return await _unitOfWork.IUserRepository.UserEmailAlreadyExit(Name);
    }

    public async Task<User> UserEmailAlreadyExitForVerify(string emailAddress)
    {
        return await _unitOfWork.IUserRepository.UserEmailAlreadyExitForVerify(emailAddress);
    }

    public Task<bool> UserNameAlreadyExit(string Name)
    {
        throw new NotImplementedException();
    }
    public Task<bool> UserPhoneAlreadyExit(string Name)
    {
        throw new NotImplementedException();
    }

    public async Task<EmailVerificationCode> VerifyEmailCodeAndEmail(EmailVerificationCode model)
    {
        var userData = await _unitOfWork.IUserRepository.verifyEmailCodeAndEmail(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<EmailVerificationCode> verifyEmailCodeAndEmailCheck(string emailAddress)
    {
       return await _unitOfWork.IUserRepository.verifyEmailCodeAndEmailCheck(emailAddress);
    }

    public async Task<bool> ActiveOrDeactiveUser(UserActiveModel model)
    {
        return await _unitOfWork.IUserRepository.ActiveOrDeactiveUser(model);
    }

    public async Task<bool> UserHaveDeleted(string Email)
    {
        return await _unitOfWork.IUserRepository.UserHaveDeleted(Email);
    }

    public async Task<bool> AssignRoleToUser(AssignRoleToUserModel model)
    {
       return await _unitOfWork.IUserRepository.AssignRoleToUser(model);    
    }
}
 
