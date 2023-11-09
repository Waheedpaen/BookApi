

   namespace ImplementDAL.Reporsitory;

using DataAccessLayer.IRepositories;
using DataAccessLayer.Seeds;
using EntitiesClasses.Entities;
using HelperData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModel.ViewModels.UserViewModel;
using ViewModels.UserViewModel;

public  class UserRepository :  Reporsitory<User, int>, IUserRepository
    {
    protected int _LoggedIn_UserID = 0;
    protected int _LoggedIn_UserTypeId = 0;
    protected string _LoggedIn_UserName = "";
    protected string _LoggedIn_UserRole = "";
    protected int _LoggedIn_RestaurantBranchId = 0;

    public ServiceResponse<object> _serviceResponse;
    public DataContexts DataContexts => Context as DataContexts;
    public UserRepository(DataContexts context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _serviceResponse = new ServiceResponse<object>();
        //_LoggedIn_UserID = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(Enum.Cl.UserId.ToString()));
        //_LoggedIn_UserTypeId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirstValue(Enum.ClaimType.UserTypeId.ToString()));
        //_LoggedIn_UserName = httpContextAccessor.HttpContext.User.FindFirstValue(Enum.ClaimType.Name.ToString())?.ToString();
      

        //if (_LoggedIn_UserID == 0)
        //    throw new Exception(CustomMessage.UserNotLoggedIn);
    }

    public UserRepository(DataContexts context) : base(context)
    {
        _serviceResponse = new ServiceResponse<object>();
        
    }


    public async Task<User> Logout(int loginUserId)
    {
        //When user log out . isonlined will null and offilne is fase
        // offline is false mean . user is offline and 
     var data =     await Context.Set<User>().FirstOrDefaultAsync(data=>data.Id == loginUserId);
        data.IsOnlined = null; 
        data.Offline = false;
        data.LoginTime = null;
        data.LastLogout = DateTime.Now;
        Context.Set<User>().Update(data);
        Context.SaveChanges();
        data.LastActive = null;
        data.LastLogout = DateTime.Now;
        return data;
    }

    public async   Task<LoginUserDto> Login(UserDtoLogin model)
    {
       
        ServiceResponse<object> serviceResponse = new();
        var objUser = await Context.Set<User>().Include(data=>data.UserTypes).Where(data => data.Email.ToLower() == model.Email.ToLower().Trim()&& (data.UserTypesId == model.UserTypeId) && (data.IsDeleted == false)).FirstOrDefaultAsync();
        if (objUser == null)
            return null;

        //var userForOnLine = new  User();
        //userForOnLine.IsOnlined = true;
        //userForOnLine.Offline = false;
        // start 
        // when user login in  isonlined is true. it mean user is isonlined . 
        // longin time. it mean when user is login . show it login time of user too 
        // and apply update method here for inonline true and assign the ligin time 
        objUser.IsOnlined = true;
        objUser.LoginTime= DateTime.Now; 
        objUser.Offline = null; 
        DataContexts.Users.Update(objUser);
        DataContexts.SaveChanges();


        //end 
        LoginUserDto obj = new();
        obj.Id = objUser.Id;
       obj.Email = objUser.Email;
        obj.Role = objUser.UserTypes.UserType;
        obj.UserName = objUser.UserName;
        obj.UserTypeId = objUser.UserTypesId != 0 ? objUser.UserTypesId :  Context.Set<User>().FirstOrDefault(data => data.Id == objUser.Id).UserTypesId;
        obj.PasswordHash = objUser.PasswordHash;
        obj.ImageUrl = objUser.ImageUrl;
        obj.PasswordSalt = objUser.PasswordSalt;

        //obj.LastActive = DateTime.UtcNow;

        if (objUser != null)
        {

        }
        
        if (!Seed.VerifyPasswordHash(model.Password.Trim(), objUser.PasswordHash, objUser.PasswordSalt))
            return null;

        return obj;
    }

    public async Task<ServiceResponse<object>> AddUser(UserAddDto model)
    {
        var objUser = await Context.Set<User>().Where(data => data.UserName.Trim() == model.Username.Trim()
        ||data.Email.Trim() == model.Email.Trim() 

        )
           .Select(data=>new
           {
               username = data.UserName,
               email = data.Email,
           }) .FirstOrDefaultAsync();
        if(objUser != null)
        {
            if (  objUser.username.Length > 0 && model.Username.Trim() == objUser.username)
            {
                _serviceResponse.Message = CustomMessage.UserAlreadyExist;
            }
            else if (objUser.email.Length > 0 && model.Email.Trim() == objUser.email)
            {
                _serviceResponse.Message = CustomMessage.EmailAlreadyExist;
            }
            _serviceResponse.Success = false;
        }
        else
            {
                User user = new();
                user.Name = model.FullName;
                user.UserName = model.Username;
                user.Email = model.Email; 
                user.UserTypesId = model.UserTypeId.ToNotNull_Int();
                user.IsDeleted = false;
            // when user crete new account by default isonline and offline will false, reason because user just create account . not login yet 
            user.IsOnlined = false;
            user.Offline = false;
            // end
                user.Updated_At = null;
                user.LastActive = DateTime.UtcNow;
                user.ImageUrl = model.ImageUrl;
                byte[] passwordHash, passwordSalt;
                Seed.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt; 
                if (model.Username != null && model.UserTypeId != null)
                {
                    await Context.Set<User>().AddAsync(user);

                    _serviceResponse.Success = true;
                    _serviceResponse.Message = CustomMessage.Added;
                }
                else
                {
                    _serviceResponse.Success = false;
                    _serviceResponse.Message = CustomMessage.Invalid;
                }
            }
          
            return _serviceResponse;

    }

    public async Task<bool> UserAlreadyExit(string Name)
    {
     
        var data = await Context.Set<User>().AnyAsync(x => x.UserName.ToLower() == Name.ToLower());
        return data == true ? true : false;
        //if (data == true)
        //    return true;
        //else
        //    return false;
    }
    public async Task<bool> UserHaveDeleted(string Email)
    {

        var data = await Context.Set<User>().AnyAsync(x => x.Email.ToLower() == Email.ToLower() && x.IsDeleted == true);
        return data == true ? true : false;
        //if (data == true)
        //    return true;
        //else
        //    return false;
    }
    public async Task<bool> UserNameAlreadyExit(string Name)
    {

        bool isExit = false;
        if(await Context.Set<User>().AnyAsync(x=>x.UserName.ToLower() == Name.ToLower()))
        {
            isExit = true;
        }
        return isExit;
    }

    public async Task<bool> UserEmailAlreadyExit(string UserMail)
    {
         bool MailExit = false;
        if(await Context.Set<User>().AllAsync(x=>x.Email == UserMail))
        {
            MailExit = true;
        }
        return MailExit;
    }

    public async Task<bool> UserPhoneAlreadyExit(string Name)
    {
        bool PhoneExit = false;
       
        return PhoneExit;
    }

    public async Task<List<User>> GetActiveUsers()
    {
       return await Context.Set<User>().Include(data=>data.UserTypes).ToListAsync();
    }

    public async Task<List<User>> GetDeActiveUsers()
    {
        return await Context.Set<User>().Include(data => data.UserTypes).ToListAsync();
    }

    public async Task<int> GetUserCount()
    {
        var  totalUserCount  = await Context.Set<User>().Where(data => data.IsOnlined == true).ToListAsync();
        return totalUserCount.Count();
    }

    public async Task<User> GetUser(int userId)
    {
        return await Context.Set<User>().Include(data => data.UserTypes).FirstOrDefaultAsync(data => data.Id == userId);
    }

    public async Task<List<User>> SearchingData(string name)
    {
          return await Context.Set<User>().Include(data=>data.UserTypes).Where(data => data.UserName.StartsWith(name)).ToListAsync();
    }

    public async Task<List<UserTypes>> GetUserTypes()
    {
        return await Context.Set<UserTypes>().ToListAsync();
    }

    public async Task<EmailVerificationCode> verifyEmailCodeAndEmail(EmailVerificationCode model)
    {
       
        await Context.Set<EmailVerificationCode>().AddAsync(model);
        return model;
      
    }

    public async Task<User> UserEmailAlreadyExitForVerify(string emailAddress)
    {
        var data = await Context.Set<User>().Where(data => data.Email == emailAddress).FirstOrDefaultAsync();
        return data;
    }

    public async Task<EmailVerificationCode> verifyEmailCodeAndEmailCheck(string emailAddress)
    {
         var data = await Context.Set<EmailVerificationCode>().Where(x => x.Email == emailAddress).FirstOrDefaultAsync();
        return data;
    }

    public async Task<bool> ActiveOrDeactiveUser(UserActiveModel model)
    {
        // this methos user: active the user or deactive the user. than 
        // if user is active than user than sigin to page . or user is deactive mean user is not login to login page
        var data = await Context.Set<User>().Where(x => x.Id == model.Id ).FirstOrDefaultAsync(); 
        data.IsDeleted = model.IsDeleted;
        Context.Set<User>().Update(data);
        Context.SaveChanges();
        return model.IsDeleted;
    }

    public async Task<bool> AssignRoleToUser(AssignRoleToUserModel model)
    {
        // assgin role to the user. user can be adim . or user..
        var data = await Context.Set<User>().Where(x => x.Id == model.Id ).FirstOrDefaultAsync();
        if (data != null)
        {
            data.UserTypesId = model.UserTypesId;
            Context.Set<User>().Update(data);
            Context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        } 
    }
}
 
