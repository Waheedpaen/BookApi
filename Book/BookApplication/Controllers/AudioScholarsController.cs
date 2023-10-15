using HelperDatas.PaginationsClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.CommonViewModel;

namespace BookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioScholarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILookUpServices _lookUpServices;
        private readonly IConfiguration _config;
        private readonly IAudioScholarsServices _audioScholarsServices;
        public AudioScholarsController(IAudioScholarsServices audioScholarsServices, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
        {
            _lookUpServices = lookUpServices;
            _mapper = mapper;
            _config = config;
            _audioScholarsServices = audioScholarsServices;
        }


        [HttpPost("SaveAudioScholars")]
        public async Task<IActionResult> SaveAudioScholars(CommonDto model)
        {
            var enity = _mapper.Map<AudioScholars>(model);
            var dataExit = await _audioScholarsServices.AudioScholarsAlreadyExit(enity.Name);
            if (dataExit != null)
            {
                return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
            }
            else
            {
                await _audioScholarsServices.Create(enity);
                return Ok(new { Success = true, Message = CustomMessage.Added });
            }
        }

        [HttpGet("AudioScholarsDetail/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = await _audioScholarsServices.Get(Id);
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

        [HttpDelete("DeleteAudioScholars/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = await _audioScholarsServices.Get(Id);

            if (entity != null)
            {
                await _audioScholarsServices.Delete(entity);
                return Ok(new { Success = true, Message = CustomMessage.Deleted });
            }
            else
            {
                return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
            }
        }



        [HttpPut("UpdateAudioScholars")]
        public async Task<IActionResult> UpdateAudioScholars(CommonDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<AudioScholars>(model);
            var dataAlreadyExits = await _audioScholarsServices.AudioScholarsAlreadyExit(entity.Name);
            if (dataAlreadyExits != null)
            {
                return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
            }
            else
            {
                var detailOldData = await _audioScholarsServices.Get(Convert.ToInt16(model.Id));
                var newData = _mapper.Map<AudioScholars>(model);
                if (detailOldData != null)
                {
                    await _audioScholarsServices.Update(detailOldData, newData);
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
            var pagedResult = await _audioScholarsServices.SearchAndPaginateAsync(options);
            return Ok(pagedResult);
        }
    }
}
