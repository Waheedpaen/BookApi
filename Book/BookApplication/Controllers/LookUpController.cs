using Azure;
using DataAccessLayer.IUnitofWork;
using DataAccessLayer.Services;
using HelperData;
using ImplementDAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModels.UserViewModel;
using ViewModels.CommonViewModel;
using ViewModels.NewsViewModel;
using ViewModels.ScholarViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LookUpController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    public LookUpController(ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _config = config;
    }

    [HttpGet("BookCategories")]
    public async Task<IActionResult> BookCategories()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.BookCategories();
        var model = _mapper.Map<List<CommonDto>>(enityData);
        if (enityData != null)
        {
            return Ok(new { Success = true, data = model, });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }


    [HttpGet("FarqaCategories")]
    public async Task<IActionResult> FarqaCategories()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.FarqaCategories();
        var model = _mapper.Map<List<FarqaCategoryDto>>(enityData);
        if (model != null)
        {
            return Ok(new { Success = true, data = model, });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty    , });
        }
    }
     

    [HttpGet("GetScholars")]
    public async Task<IActionResult> GetScholars()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.GetScholars();
        var model = _mapper.Map<List<ScholarDto>>(enityData);
        if (model != null)
        {
            return Ok(new { Success = true, data = model, });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }
 
    [HttpGet("GetBookDetails")]
    public async Task<IActionResult> GetBookDetails()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.GetBookDetails();
        //var model = _mapper.Map<List<ScholarDto>>(enityData);//it dto have remained
        if (enityData != null)
        {
            return Ok(new { Success = true, data = enityData, });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }
    [HttpGet("GetBookImages")]
    public async Task<IActionResult> GetBookImages()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.GetBookImages();
        //var model = _mapper.Map<List<ScholarDto>>(enityData);//it dto have remained
        if (enityData != null)
        {
            return Ok(new { Success = true, data = enityData, });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }

    [HttpGet("GetMadrassaBooks")]
    public async Task<IActionResult> GetMadrassaBooks()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.GetMadrassaBooks();
        //var model = _mapper.Map<List<ScholarDto>>(enityData);//it dto have remained
        if (enityData != null)
        {
            return Ok(new { Success = true, data = enityData, });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }
     
     [HttpGet("GetMonthlyMagzines")]
    public async Task<IActionResult> GetMonthlyMagzines()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.GetMonthlyMagzines();
        var model = _mapper.Map<List<CommonDto>>(enityData);
        if (model != null)
        {
            return Ok(new { Success = true, data = model });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }
    [HttpGet("GetMonthlyMagzinesNobiles")]
    public async Task<IActionResult> GetMonthlyMagzinesNobiles()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.GetMonthlyMagzines();
        var model = _mapper.Map<List<CommonDto>>(enityData);
        model =  model.Select(a=>a).TakeLast(5).ToList();
        if (model != null)
        {
            return Ok(new { Success = true, data = model });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }

    [HttpGet("GetAudioScholars")]
    public async Task<IActionResult> AudioScholars()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.AudioScholars();
        var model = _mapper.Map<List<CommonDto>>(enityData); 
        if (model != null)
        {
            return Ok(new { Success = true, data = model });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }

    [HttpGet("GetAudioDetails")]
    public async Task<IActionResult> GetAudioDetails()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.AudioDetails();
        var model = _mapper.Map<List<CommonDto>>(enityData); 
        if (model != null)
        {
            return Ok(new { Success = true, data = model });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }

    [HttpGet("GetNews")]
    public async Task<IActionResult> GetNews()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var enityData = await _lookUpServices.News();
        var model = _mapper.Map<List<NewsDto>>(enityData);
        if (model != null)
        {
            return Ok(new { Success = true, data = model });
        }
        else
        {
            return Ok(new { Success = false, data = string.Empty, });
        }
    }
}