

using HelperDatas.PaginationsClasses;
using ViewModels.CommonViewModel;
using ViewModels.ScholarViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScholarController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IScholarServices  _scholarServices  ;
    public ScholarController(IScholarServices scholarServices, IMapper mapper, IConfiguration config)
    {
        _mapper = mapper;
        _config = config;
        _scholarServices = scholarServices;
    }

    [HttpPost("SaveScholar")]
    public async Task<IActionResult> SaveScholar(ScholarSaveDto model)
    {
        var enity = _mapper.Map<Scholar>(model);
        var dataExit = await _scholarServices.ScholarNameAlreadyExit(enity.Name);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
        }
        else
        {
            await _scholarServices.Create(enity);
            return Ok(new { Success = true, Message = CustomMessage.Added });
        }
    }

     
    [HttpGet("ScholarDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _scholarServices.Get(Id);
        var model = _mapper.Map<ScholarDto>(entity);
        if (model != null)
        {
            return Ok(new { Data = model, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }
    }

    [HttpDelete("DeleteScholar/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _scholarServices.Get(Id);

        if (entity != null)
        {
            await _scholarServices.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }

    [HttpPut("UpdateScholar")]
    public async Task<IActionResult> UpdateScholar(ScholarDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<Scholar>(model);
        var dataAlreadyExits = await _scholarServices.ScholarNameAlreadyExit(entity.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            var detailOldData = await _scholarServices.Get(model.Id);
            var newData = _mapper.Map<Scholar>(model);
            if (detailOldData != null)
            {
                await _scholarServices.Update(detailOldData, newData);
                return Ok(new { Success = true, Message = CustomMessage.Updated });
            }
            else
            {
                return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
            }
        }
    }

    [HttpGet("GetScholarByFarqaCategories/{Id}")]
    public async Task<IActionResult> GetScholarByFarqaCategories(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _scholarServices.GetScholarByFarqaCategories(Id);
        var model = _mapper.Map<List<ScholarDto>>(entity);
        if (model != null)
        {
            return Ok(new { Data = model, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }
    }
    [HttpGet("SearchAndPaginateAsync")] 
    public async Task<IActionResult> SearchAndPaginateAsync([FromQuery] SearchAndPaginateOptions options)
    {
        var pagedResult = await _scholarServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }



}
