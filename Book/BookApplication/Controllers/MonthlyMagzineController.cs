using HelperDatas.PaginationsClasses;
using ImplementDAl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ViewModels.CommonViewModel;
using ViewModels.MonthlyMagizne;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MonthlyMagzineController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    private readonly IMonthlyMagzinesServices _monthlyMagzinesServices;
    private readonly IWebHostEnvironment _hostEnvironment;
    public MonthlyMagzineController(IWebHostEnvironment hostEnvironment, IMonthlyMagzinesServices monthlyMagzinesServices, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
        _config = config;
        _monthlyMagzinesServices = monthlyMagzinesServices; 
    }
    [HttpPost("SaveMonthlyMagzines")]
    //[DisableRequestSizeLimit]
    //[RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    public async Task<IActionResult> SaveMonthlyMagzines([FromForm] MonthlyMagzinesSaveDto model)
    {
        var enity = _mapper.Map<MonthlyMagzine>(model);
        var dataExit = await _monthlyMagzinesServices.MonthlyMagzinesAlreadyExit(enity.Name);
        if (dataExit != null)
        {
            return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
        }
        else
        {
            {
                if (model.PdfFile == null || model.PdfFile.Length <= 0)
                {
                    return BadRequest("No file or empty file provided.");
                }
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "MonthlyMagzine");
                var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PdfFile.FileName);
                var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await model.PdfFile.CopyToAsync(pdfStream);
                }
                var imageDetail = new MonthlyMagzine()
                {
                    FileNamePDF = pdfFileName,
                    FilePathPDF = pdfFilePath,
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                };
                await _monthlyMagzinesServices.Create(imageDetail);
                return Ok(new { Success = true, Message = CustomMessage.Added });
            }
        }
    }
    //it used to save audio 
    //[HttpPost("SaveMonthlyMagzines")]
    //[DisableRequestSizeLimit]
    //[RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
    //public async Task<IActionResult> SaveMonthlyMagzines([FromForm] MonthlyMagzinesSaveDto model)
    //{
    //    var enity = _mapper.Map<MonthlyMagzine>(model);
    //    var dataExit = await _monthlyMagzinesServices.MonthlyMagzinesAlreadyExit(enity.Name);
    //    if (dataExit != null)
    //    {
    //        return Ok(new { Success = false, Message = dataExit.Name + ' ' + "Already Exist", });
    //    }
    //    else
    //    {
    //        {
    //            if (model.PdfFile == null || model.PdfFile.Length <= 0)
    //            {
    //                return BadRequest("No file or empty file provided.");
    //            }
    //            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "MonthlyMagzine");
    //            var pdfFileName = Guid.NewGuid().ToString() + ".mp3";
    //            var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
    //            if (!Directory.Exists(uploadsFolder))
    //            {
    //                Directory.CreateDirectory(uploadsFolder);
    //            }
    //            using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
    //            {
    //                await model.PdfFile.CopyToAsync(pdfStream);
    //            }
    //            var imageDetail = new MonthlyMagzine()
    //            {
    //                FileNamePDF = pdfFileName,
    //                FilePathPDF = pdfFilePath,
    //                Name = model.Name,
    //                Description = model.Description,
    //                ImageUrl = model.ImageUrl,
    //            };
    //            await _monthlyMagzinesServices.Create(imageDetail);
    //            return Ok(new { Success = true, Message = CustomMessage.Added });
    //        }
    //    }
    //}
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
    public async Task<IActionResult> UpdateMonthlyMagzines([FromForm] MonthlyMagzinesSaveDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entity = _mapper.Map<MonthlyMagzine>(model);
        var dataAlreadyExits = await _monthlyMagzinesServices. MonthlyMagzinesAlreadyExit(entity.Name);
        if (dataAlreadyExits != null)
        {
            return Ok(new { Success = false, Message = dataAlreadyExits.Name + ' ' + "Already Exist" });
        }
        else
        {
            if(model.PdfFile == null)
            {
                var detailOldData1 = await _monthlyMagzinesServices.Get(Convert.ToInt16(model.Id));
                var monthlyMagzines = new MonthlyMagzine()
                {
                  
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                };
                if (monthlyMagzines != null)
                {
                    await _monthlyMagzinesServices.UpdateForwithoutPdf(detailOldData1, monthlyMagzines);
                    return Ok(new { Success = true, Message = CustomMessage.Updated });
                }
                else
                {
                    return Ok(new { Success = false, Message = CustomMessage.RecordNotFound });
                }
            }
            else
            {
                var detailOldData = await _monthlyMagzinesServices.Get(Convert.ToInt16(model.Id));
                //var newData = _mapper.Map<MonthlyMagzine>(model);
                if (model.PdfFile == null || model.PdfFile.Length <= 0)
                {
                    return BadRequest("No file or empty file provided.");
                }
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "MonthlyMagzine");
                var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PdfFile.FileName);
                var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await model.PdfFile.CopyToAsync(pdfStream);
                }
                var imageDetail = new MonthlyMagzine()
                {
                    FileNamePDF = pdfFileName,
                    FilePathPDF = pdfFilePath,
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                };
                if (detailOldData != null)
                {
                    await _monthlyMagzinesServices.Update(detailOldData, imageDetail);
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
        var pagedResult = await _monthlyMagzinesServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }


    [HttpGet("GetMonthlyMagzinesPdf/{id}")]
    public async Task<IActionResult> GetMonthlyMagzinesPdf(int id)
    {
        var entity = await _monthlyMagzinesServices.Get(id);

        if (entity == null)
        {
            return NotFound();
        }

        var fileStream = new FileStream(entity.FilePathPDF, FileMode.Open, FileAccess.Read);

        return File(fileStream, "application/pdf", entity.FileNamePDF);
    }






    [HttpPost("dara")] 
    [DisableRequestSizeLimit]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)] 
    public async Task<ActionResult> UploadFileAsync(IFormFile file)
    {
        if (file == null)
            return Ok(new { success = false, message = "You have to attach a file" });

        var fileName = file.FileName;
        // var extension = Path.GetExtension(fileName);

        // Add validations here...

        var localPath = $"{Path.Combine(System.AppContext.BaseDirectory, "myCustomDir")}\\{fileName}";

        // Create dir if not exists
        Directory.CreateDirectory(Path.Combine(System.AppContext.BaseDirectory, "myCustomDir"));

        using (var stream = new FileStream(localPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // db.SomeContext.Add(someData);
        // await db.SaveChangesAsync();

        return Ok(new { success = true, message = "All set", fileName });
    }
}
