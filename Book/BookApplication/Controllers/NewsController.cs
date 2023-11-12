using HelperDatas.PaginationsClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.CommonViewModel;
using ViewModels.NewsViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly INewServices _newServices;
    private int _LoggedIn_UserID = 0;
    public NewsController(INewServices newServices,  IMapper mapper)
    { 
        _mapper = mapper;
        _newServices = newServices;
    }


    [HttpPost("SaveNews")]
    public async Task<IActionResult> SaveNews(NewsDto model)
    {
        var enity = _mapper.Map<News>(model);
        var dataExit = await _newServices.NewsAlreadyExit(enity.Header);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Header + ' ' + "Already Exist", });
        }
        else
        {
            await _newServices.Create(enity);
            return Ok(new { Success = true, Message = CustomMessage.Added });
        }
    }

    [HttpGet("NewsDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _newServices.Get(Id);
        var model = _mapper.Map<NewsDto>(entity);
        if (model != null)
        {
            return Ok(new { Data = model, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }

    }

    [HttpDelete("DeleteNews/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _newServices.Get(Id);

        if (entity != null)
        {
            await _newServices.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }

    [HttpGet("SearchAndPaginateCategories")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions options)
    {
        var pagedResult = await _newServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }


    [HttpPut("UpdateNews")]
    public async Task<IActionResult> UpdateNews(NewsDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<News>(model);
        var dataAlreadyExits = await _newServices. NewsAlreadyExit(entity.Header);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Header + ' ' + "Already Exist" });
        }
        else
        {
            var detailOldData = await _newServices.Get(Convert.ToInt16(model.Id));
            var newData = _mapper.Map<News>(model);
            if (detailOldData != null)
            {
                await _newServices.Update(detailOldData, newData);
                return Ok(new { Success = true, Message = CustomMessage.Updated });
            }
            else
            {
                return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
            }
        }
    }
}
