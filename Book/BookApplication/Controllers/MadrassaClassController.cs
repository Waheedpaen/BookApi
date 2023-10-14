using DataAccessLayer.IRepositories;
using HelperDatas.PaginationsClasses;
using ImplementDAl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.CommonViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MadrassaClassController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    private readonly IMadrassaClassService _madrassaClassService;
    public MadrassaClassController(IMadrassaClassService madrassaClassService, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _config = config;
        _madrassaClassService = madrassaClassService;
    }


    [HttpPost("SaveMadrassClass")]
    public async Task<IActionResult> SaveMadrassClass(CommonDto model)
    {
        var enity = _mapper.Map<MadrassaClass>(model);
        var dataExit = await _madrassaClassService.MadrassaBooklAlreadyExit(enity.Name);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
        }
        else
        {
            await _madrassaClassService.Create(enity);
            return Ok(new { Success = true, Message = CustomMessage.Added });
        }
    }

    [HttpGet("MadrassaClassDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _madrassaClassService.Get(Id);
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

    [HttpDelete("DeleteMadrassClass/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _madrassaClassService.Get(Id);

        if (entity != null)
        {
            await _madrassaClassService.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }



    [HttpPut("UpdateMadrassClass")]
    public async Task<IActionResult> UpdateMadrassClass(CommonDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<MadrassaClass>(model);
        var dataAlreadyExits = await _madrassaClassService.MadrassaBooklAlreadyExit(entity.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            var detailOldData = await _madrassaClassService.Get(Convert.ToInt16(model.Id));
            var newData = _mapper.Map<MadrassaClass>(model);
            if (detailOldData != null)
            {
                await _madrassaClassService.Update(detailOldData, newData);
                return Ok(new { Success = true, Message = CustomMessage.Updated });
            }
            else
            {
                return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
            }
        }
    }


    [HttpGet("SearchAndPaginateCategories")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions options)
    {
        var pagedResult = await _madrassaClassService.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }
}
