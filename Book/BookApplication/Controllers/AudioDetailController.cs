using HelperDatas.PaginationsClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.AudioDetailViewModel;

using System;
using System.IO;
using ViewModels.CommonViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AudioDetailController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    private readonly IAudioDetailServices _audioDetailServices;
    private readonly IWebHostEnvironment _hostEnvironment;
    public AudioDetailController(IWebHostEnvironment hostEnvironment, IAudioDetailServices audioDetailServices, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
        _config = config;
        _audioDetailServices = audioDetailServices;
    }
    [HttpPost("SaveAudioDetail")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<IActionResult> SaveAudioDetail([FromForm] AudioDetailSaveViewModel model)
    {
        //var enity = _mapper.Map<MonthlyMagzine>(model);
        var dataExit = await _audioDetailServices.AudioDetailAlreadyExit(model.Name);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
        }
        else
        {
            {
                if (model.ImageUrlData == null || model.ImageUrlData.Length <= 0)
                {
                    return BadRequest("No file or empty file provided.");
                }
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "AudioDetail");
                var audioFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageUrlData.FileName);
                var audioFileNameFilePath = Path.Combine(uploadsFolder, audioFileName);
                if (!Directory.Exists(uploadsFolder))
                {   
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var pdfStream = new FileStream(audioFileNameFilePath, FileMode.Create))
                {
                    await model.ImageUrlData.CopyToAsync(pdfStream);
                }
                var imageDetail = new AudioDetail()
                {
                    FileNameAudio = audioFileName,
                    FilePathAudio = audioFileNameFilePath,
                    Name = model.Name,
                    AudioScholarsId = Convert.ToInt32(model.AudioScholarsId),
                    DateRelase = model.DateRelase,
                };
                await _audioDetailServices.Create(imageDetail);
                return Ok(new { Success = true, Message = CustomMessage.Added });
            }
        }
    }
    [HttpGet("AudioDetail/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _audioDetailServices.Get(Id);
        //var model = _mapper.Map<AudioDetailDto>(entity);
        if (entity != null)
        {
            return Ok(new { Data = entity, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }

    }
    [HttpGet("GetAudio/{Id}")]
    public async Task<IActionResult> GetAudio(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var audioDetail = await _audioDetailServices.Get(Id);

        if (audioDetail == null)
        {
            return NotFound();
        }

        var filePath = audioDetail.FilePathAudio; // Adjust the file path according to your implementation

        

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      
 

        return File(fileStream, "audio/mp3"); // Adjust the MIME type if needed
    }
    [HttpDelete("DeleteAudioDetail/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _audioDetailServices.Get(Id);

        if (entity != null)
        {
            await _audioDetailServices.Delete(entity);
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }



    [HttpPut("UpdateAudioDetail")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<IActionResult> UpdateAudioDetail([FromForm] AudioDetailSaveViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        //var entity = _mapper.Map<AudioDetail>(model);
        var dataAlreadyExits = await _audioDetailServices.AudioDetailAlreadyExit(model.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            if (model.ImageUrlData == null)
            {
                var detailOldData1 = await _audioDetailServices.Get(Convert.ToInt16(model.Id));
                var audioDetail = new AudioDetail()
                {

                    Name = model.Name,
                    DateRelase = model.DateRelase,
                    AudioScholarsId = Convert.ToInt32(model.AudioScholarsId),
                };
                if (audioDetail != null)
                {
                    await _audioDetailServices.UpdateForAudioDetail(detailOldData1, audioDetail);
                    return Ok(new { Success = true, Message = CustomMessage.Updated });
                }
                else
                {
                    return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
                }
            }
            else
            {
                var detailOldData = await _audioDetailServices.Get(Convert.ToInt16(model.Id));
                //var newData = _mapper.Map<MonthlyMagzine>(model);
                if (model.ImageUrlData == null || model.ImageUrlData.Length <= 0)
                {
                    return BadRequest("No file or empty file provided.");
                }
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "AudioDetail");
                var audioFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageUrlData.FileName);
                var audioFileNameFilePath = Path.Combine(uploadsFolder, audioFileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var pdfStream = new FileStream(audioFileNameFilePath, FileMode.Create))
                {
                    await model.ImageUrlData.CopyToAsync(pdfStream);
                }
                var imageDetail = new AudioDetail()
                {
                    FileNameAudio = audioFileName,
                    FilePathAudio = audioFileNameFilePath,
                    Name = model.Name,
                    AudioScholarsId = Convert.ToInt32(model.AudioScholarsId),
                    DateRelase = model.DateRelase,
                };

                if (imageDetail != null)
                {
                    await _audioDetailServices.Update(detailOldData, imageDetail);
                    return Ok(new { Success = true, Message = CustomMessage.Updated });
                }
                else
                {
                    return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
                }
            }

        }
    }


    [HttpGet("SearchAndPaginateCategories")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions options)
    {
        var pagedResult = await _audioDetailServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }



    [HttpGet("GetAudioDetailByAudioScholar/{Id}")]
    public async Task<IActionResult> GetAudioDetailByAudioScholar(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = await _audioDetailServices. GetAudioDetailByAudioScholar(Id);
      /*  var model = _mapper.Map<List<CommonDto>>(entity)*/;
        if (entity != null)
        {
            return Ok(new { Data = entity, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }
    }
}
