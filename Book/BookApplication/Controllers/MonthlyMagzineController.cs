using HelperDatas.PaginationsClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.CommonViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MonthlyMagzineController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    private readonly IMonthlyMagzinesServices _monthlyMagzinesServices;
    public MonthlyMagzineController(IMonthlyMagzinesServices monthlyMagzinesServices, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _config = config;
        _monthlyMagzinesServices = monthlyMagzinesServices;
    }
    [HttpPost("SaveMonthlyMagzines")]
    public async Task<IActionResult> SaveMonthlyMagzines(CommonDto model)
    {
        var enity = _mapper.Map<MonthlyMagzine>(model);
        var dataExit = await _monthlyMagzinesServices. MonthlyMagzinesAlreadyExit(enity.Name);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
        }
        else
        {
            await _monthlyMagzinesServices.Create(enity);
            return Ok(new { Success = true, Message = CustomMessage.Added });
        }
    }

    [HttpGet("MonthlyMagzinesDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _monthlyMagzinesServices.Get(Id);
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

    [HttpDelete("DeleteMonthlyMagzines/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _monthlyMagzinesServices.Get(Id);

        if (entity != null)
        {
            await _monthlyMagzinesServices.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }



    [HttpPut("UpdateMonthlyMagzines")]
    public async Task<IActionResult> UpdateMonthlyMagzines(CommonDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<BookCategory>(model);
        var dataAlreadyExits = await _monthlyMagzinesServices. MonthlyMagzinesAlreadyExit(entity.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            var detailOldData = await _monthlyMagzinesServices.Get(Convert.ToInt16(model.Id));
            var newData = _mapper.Map<MonthlyMagzine>(model);
            if (detailOldData != null)
            {
                await _monthlyMagzinesServices.Update(detailOldData, newData);
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
        var pagedResult = await _monthlyMagzinesServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }
}
