
using HelperDatas.PaginationsClasses;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class FarqaCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IFarqaCategoryServices  _farqaCategoryServices ;
        public FarqaCategoryController(IFarqaCategoryServices farqaCategoryServices, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
            _farqaCategoryServices = farqaCategoryServices;
        }



    [HttpGet("farqaCategoryDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _farqaCategoryServices.Get(Id);
        var model = _mapper.Map<FarqaCategoryDto>(entity);
        if (model != null)
        {
            return Ok(new { Data = model, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }
    }


    [HttpDelete("DeleteFarqaCategory/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _farqaCategoryServices.Get(Id);

        if (entity != null)
        {
            await _farqaCategoryServices.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }


    [HttpPost("SaveFarqaCategory")]
    public async Task<IActionResult> SaveBookCategory(FarqaCategorySaveDto model)
    {
        var enity = _mapper.Map<FarqaCategory>(model);
        var dataExit = await _farqaCategoryServices.FarqaCategoryAlreadyExit(enity.Name);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
        }
        else
        {
            await _farqaCategoryServices.Create(enity);
            return Ok(new { Success = true, Message = CustomMessage.Added });
        }
    }



    [HttpPut("UpdateFarqaCategory")]
    public async Task<IActionResult> UpdateFarqaCategory(FarqaCategoryDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<FarqaCategory>(model);
        var dataAlreadyExits = await _farqaCategoryServices.FarqaCategoryAlreadyExit(entity.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            var detailOldData = await _farqaCategoryServices.Get(model.Id);
            var newData = _mapper.Map<FarqaCategory>(model);
            if (detailOldData != null)
            {
                await _farqaCategoryServices.Update(detailOldData, newData);
                return Ok(new { Success = true, Message = CustomMessage.Updated });
            }
            else
            {
                return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
            }
        }
    }

    [HttpGet("GetFarqaCategoriesByBook/{Id}")]
    public async Task<IActionResult> GetFarqaCategoriesByBook(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _farqaCategoryServices.GetFarqaCategoriesByBook(Id);
        var model = _mapper.Map<List<FarqaCategoryDto>>(entity);
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
        var pagedResult = await _farqaCategoryServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }















































}

