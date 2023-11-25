using Azure;
using DataAccessLayer.Services;
using HelperData;
using HelperDatas.PaginationsClasses;
using ImplementDAl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using ViewModels.CommonViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookCategoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBookCategoryServices _bookCategoryServices;
    private int _LoggedIn_UserID = 0;
    public BookCategoryController(IHttpContextAccessor
        httpContextAccessor, IBookCategoryServices
        bookCategoryServices, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _config = config;
        _bookCategoryServices = bookCategoryServices;
       _httpContextAccessor = httpContextAccessor;

    }

        [HttpPost("SaveBookCategory")]
        public async Task<IActionResult> SaveBookCategory(CommonDto model)
        { 
            var enity = _mapper.Map<BookCategory>(model);
            var dataExit = await _bookCategoryServices.BookCategoryAlreadyExit(enity.Name);
            if (dataExit != null)
            {
                return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
            }
            else
            {
                await _bookCategoryServices.Create(enity);
                return Ok(new { Success = true, Message = CustomMessage.Added });
            }
        }

    [HttpGet("BookCategoryDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _bookCategoryServices.Get(Id);
        var model = _mapper.Map<CommonDto>(entity);
        if (model != null)
        {
            return Ok(new { Data = model, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }

    }



    [HttpGet("GetSuggestions/{searchword}")]
    public async Task<IActionResult> GetSuggestions(string searchword)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _bookCategoryServices.GetSuggestions(searchword);
       var model = _mapper.Map<List<CommonDto>>(entity);
        if (model != null)
        {
            return Ok(new { Data = model, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }

    }





    [HttpDelete("DeleteBookCategory/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _bookCategoryServices.Get(Id);

        if (entity != null)
        {
            await _bookCategoryServices.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }



    [HttpPut("UpdateBookCategory")]
    public async Task<IActionResult> UpdateBookCategory(CommonDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<BookCategory>(model);
        var dataAlreadyExits = await _bookCategoryServices.BookCategoryAlreadyExit(entity.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            var detailOldData = await _bookCategoryServices.Get( Convert.ToInt16(model.Id));
            var newData = _mapper.Map<BookCategory>(model);
            if (detailOldData != null)
            {
                await _bookCategoryServices.Update(detailOldData, newData);
                return Ok(new { Success = true, Message = CustomMessage.Updated });
            }
            else
            {
                return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
            }
        } 
    }


    [HttpGet("SearchAndPaginateCategories")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions  options)
    {
        var pagedResult = await _bookCategoryServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }



}
















 