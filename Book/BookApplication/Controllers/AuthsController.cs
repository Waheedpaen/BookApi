﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using System.Net.Mail;
using HelperData;
using DataAccessLayer.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.SignalR;
using ImplementDAL.Services;
using HelperDatas.PaginationsClasses;
using ImplementDAl.Services;
using ViewModels.CommonViewModel;
using ViewModels.UserViewModel;

namespace BookApplication.Controllers
 ;
[Route("api/[controller]")]
[ApiController]

public class AuthsController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    private readonly IHubContext<BroadcastHub> _hubContext;
    public AuthsController(IHubContext<BroadcastHub> hubContext, IUserService userService, IMapper mapper, IConfiguration config)
    {
        _userService = userService;
        _mapper = mapper;
        _config = config;
        _hubContext = hubContext;
    }

    [HttpGet("SearchAndPaginateCategories")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions options)
    {
       
        var pagedResult = await _userService.SearchAndPaginateAsync(options); 

        return Ok(pagedResult);
    }
    [HttpGet("GetActiveUsers")]
    public async Task<IActionResult> ActiveUsers()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _userService.ActiveUsers();
        List<UserListDto> listUser = new();
        foreach (var item in enityData.Where(data => data.IsOnlined == true).ToList())
        {
            listUser.Add(new UserListDto { Name = item.Name, LoginTime = item.LoginTime, UserTypeId = item.UserTypesId  });
        }
        //var userDto = _mapper.Map<List<UserListDto>>(enityData);

        if (listUser != null)
        {
            _response.Data = listUser;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.DataNotExit;
            return Ok(_response);
        }

    }
    [HttpGet("GetDeActiveUsers")]
        public async Task<IActionResult> GetDeActiveUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var enityData = await _userService.GetDeActiveUsers();
            List<UserListDto> listUser = new();
            foreach (var item in enityData.Where(data => data.Offline == false).ToList())
            {
                listUser.Add(new UserListDto { Name = item.Name, LastLogout = item.LastLogout, UserTypeId=item.UserTypesId });
            }
            //var userDto = _mapper.Map<List<UserListDto>>(enityData);

            if (listUser != null)
        {
            _response.Data = listUser;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.DataNotExit;
            return Ok(_response);
        }

    }
    [HttpGet("GetUserCount")]
    public async Task<IActionResult> GetUserCount()

    {
        var userCounts = await _userService.GetUserCount();

        await _hubContext.Clients.All.SendAsync("ReceiveUserCount", userCounts);

        return Ok(new { Data = userCounts, Success = true });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserDtoLogin userDtoLogin)
    {
     

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        userDtoLogin.Email = userDtoLogin.Email.ToLower();
        if (await _userService.UserHaveDeleted(userDtoLogin.Email))
            return BadRequest(new { message = CustomMessage.RecordExits });

        var objUserLogin = await _userService.Login(userDtoLogin); 
        if (objUserLogin == null)
        {
            _response.Success = false;
            _response.Message = CustomMessage.UserUnAuthorized;
            return Ok(_response);
        }
        Claim[] claims = new[]
        {
                new Claim(Enums.ClaimType.UserId.ToString(), objUserLogin.Id.ToString()),
                new Claim(Enums.ClaimType.Email.ToString(), objUserLogin.Email.ToString()),
                new Claim(Enums.ClaimType.Role.ToString(), objUserLogin.Role.ToString()),
                new Claim(Enums.ClaimType.Name.ToString(), objUserLogin.UserName.ToString()),
                new Claim(Enums.ClaimType.UserTypeId.ToString(), objUserLogin.UserTypeId.ToString()),
                new Claim(Enums.ClaimType.ImageUrl.ToString(), objUserLogin.ImageUrl.ToString()),


            };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JsonWebTokenKeys:IssuerSigningKey"]));
          var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(3), claims: claims, signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
      //  var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(1), claims: claims, signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        _response.Data = new
        {
            loggedInUserId = claims.FirstOrDefault(x => x.Type.Equals(Enums.ClaimType.UserId.ToString())).Value,
            loggedInUserName = claims.FirstOrDefault(x => x.Type.Equals(Enums.ClaimType.Name.ToString())).Value,
            token = new JwtSecurityTokenHandler().WriteToken(token),
            loggedInUserTypeId = claims.FirstOrDefault(x => x.Type.Equals(Enums.ClaimType.UserTypeId.ToString())).Value,



        };

        _response.Success = true;
        return Ok(_response);
    }

    [HttpPost("AddData")]
    public async Task<IActionResult> AddUser(UserAddDto obj)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        } 
        obj.Username = obj.Username.ToLower();
        if (await _userService.UserAlreadyExit(obj.Username))
            return BadRequest(new { message = CustomMessage.UserAlreadyExist });

        _response = await _userService.AddUser(obj);



        return Ok(_response);


    }
    //UWL ... method check exitence name 

    [HttpGet("CheckUserNameExistence/{Name}")]
    public async Task<IActionResult> CheckNameExistence(string Name)
    {
        var obj = await _userService.CheckUserNameExistence(Name);
        return Ok(obj);

    }
    [HttpGet("CheckUserName/{Name}")]
    public async Task<IActionResult> CheckUserName(string Name)
    {
        var obj = await _userService.UserAlreadyExit(Name);
        return Ok(obj);

    }
    [HttpDelete("DeleteUserData/{Id}")]
    public async Task<IActionResult> DeleteUserData(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _userService.GetUser(Id);
        if (obj != null)
        {
            await _userService.Delete(obj);
            _response.Success = true;
            _response.Message = CustomMessage.Deleted;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        }
    }

    [HttpGet("SearchData/{Name}")]
    public async Task<IActionResult> SearchData(string Name)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var osSearchData = await _userService.SearchingData(Name);
        _response.Data = osSearchData;
        _response.Success = true;
        return Ok(_response);
    }

    [HttpGet("GetUserTypes")]
    public async Task<IActionResult> GetUserTypes()
    {
        var data = await _userService.GetUserTypes();
        var dataDto = _mapper.Map<List<UserTypesDto>>(data);
        return Ok(dataDto);

    }
    [HttpGet("GetUser/{Id}")]
    public async Task<IActionResult> GetUser(int Id)
    {
        var data = await _userService.GetUser(Id);
        var dataDto = _mapper.Map<UserListDto>(data);
        if (dataDto != null)
        {
            _response.Data = data; ;
            _response.Success = true;
            return Ok(_response);

        }
        else
        {
            _response.Data = null;
            _response.Success = false;
            return Ok(_response);
        }
    }

    [HttpGet("LogOut/{Id}")]
    public async Task<IActionResult> LogOut(int Id)
    {
        var data = await _userService.Logout(Id);
        //var dataDto = _mapper.Map<UserListDto>(data);
          
        return Ok(new { Success = true   });

      
     
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UserUpdateDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var modelEntity = _mapper.Map<User>(model);
        var obj = await _userService.GetUser(modelEntity.Id);
        if (obj != null)
        {
            await _userService.UpdateUser(obj, modelEntity);
            _response.Success = true;
            _response.Message = CustomMessage.Updated;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        }

    }

    [HttpPut("UpdatePassword")]

    public async Task<IActionResult> UpdatePassword(ChangePasswordDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var obj = await _userService.GetUser(model.Id);
        if (obj != null)
        {
            await _userService.ChangePassword(obj, model);
            _response.Success = true;
            _response.Message = CustomMessage.Updated;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        }
    }
    [HttpGet("verifyEmailCodeAndEmail/{email}")]
    public async Task<IActionResult> VerifyedEmailCodeAndEmail(string email)
    {
        MailMessage mm = new MailMessage();
        SmtpClient smtp = new SmtpClient();

        var objUsers = await _userService.UserEmailAlreadyExitForVerify(email);
        if (objUsers != null)
        {

            if (objUsers.Email.Length > 0 && email.Trim() == objUsers.Email)
            {
                _response.Message = CustomMessage.EmailAlreadyExist;
            }

        }
        else
        {
            Random randomNumber = new Random();
            var randomNumberString = randomNumber.Next(0, 9999).ToString("0000");
            var ObjVerification = new EmailVerificationCode()
            {
                Email = email,
                Code = randomNumberString.ToInt32(),
            };
            await _userService.VerifyEmailCodeAndEmail(ObjVerification);
            mm.From = new MailAddress("FabIntelLahore@gmail.com", "FabIntel", Encoding.UTF8);
            mm.To.Add(new MailAddress(email));
            mm.Subject = "FabTime Email Verification Code  ";
            mm.Body = "Your Verifcation Code is " + randomNumberString;

            mm.IsBodyHtml = true;
            smtp.Host = "smtp.gmail.com";

            smtp.EnableSsl = true;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "FabIntelLahore@gmail.com";//gmail user name
            NetworkCred.Password = "oywmkwebtzlvmlrr";// password
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587; //Gmail port for e-mail 465 or 587
            smtp.Send(mm);
        }
        return Ok(_response);
    }
    [HttpGet("verifyEmailCodeAndEmailCheck/{emailAddress}/{code}")]
    public async Task<IActionResult> verifyEmailCodeAndEmailCheck(string emailAddress, int code)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var objCode = await _userService.verifyEmailCodeAndEmailCheck(emailAddress);
        if (objCode == null)
        {

            _response.Success = false;
            _response.Message = "Email/ User Name does not exist";

        }
        else
        {
            if (objCode.Code == code)
            {
                _response.Success = true;
                _response.Message = "Code Verified";

            }
        }

        return Ok(_response);

    }

    [HttpPost("UserActivitation")]
    public async Task<IActionResult> UserActivitation(UserActiveModel model)
    {
         
        var dataExit = await _userService.ActiveOrDeactiveUser(model);
      
         return Ok(new { Success = dataExit,   });
    
       
    }

    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserModel model)
    {

        var boolObject = await _userService.AssignRoleToUser(model);

        return Ok(new { Success = boolObject, });


    }
 
}

