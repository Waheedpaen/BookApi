using HelperDatas.PaginationsClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.BookDetails;
using ViewModels.MadrassaBookViewModel;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MadrassaBookController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILookUpServices _lookUpServices;
    private readonly IConfiguration _config;
    private readonly IMadrassaBookServices _madrassaBookServices;
    private readonly IWebHostEnvironment _hostEnvironment;
    public MadrassaBookController(IWebHostEnvironment hostEnvironment, IMadrassaBookServices madrassaBookServices,IBookCategoryServices bookCategoryServices, ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
    {
        _lookUpServices = lookUpServices;
        _mapper = mapper;
        _config = config;
        _madrassaBookServices = madrassaBookServices;
        _hostEnvironment = hostEnvironment;
    }

    [HttpGet("SearchAndPaginateCategories")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions options)
    {
        var pagedResult = await _madrassaBookServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }


    [HttpDelete("DeleteMadrassaBook/{Id}")]
    public async Task<IActionResult> DeleteMadrassaBook(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _madrassaBookServices.Get(Id);
        if (obj != null)
        {
            await _madrassaBookServices.DeleteMadrassaBook(obj);
            foreach (var item in obj.MadrassaBookCatgories)
            {
                await _madrassaBookServices.DeleteMadrassaBookCatgoryImagePermently(item);
            }
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }

    [HttpGet("DownloadBookPdf/{Id}")]
    public async Task<IActionResult> GetPdfById(int Id)
    {
        var pdfDocument = await _madrassaBookServices.GetMadrassaBookCatgoryById(Id);

        if (pdfDocument == null)
        {
            return NotFound();
        }

        var fileStream = new FileStream(pdfDocument.FilePathPDF, FileMode.Open, FileAccess.Read);

        return File(fileStream, "application/pdf", pdfDocument.FileNamePDF);
    }


    [HttpPost("MadrassaBookSave")]
    public async Task<IActionResult> MadrassaBookSave([FromForm] MadrassaBookSaveDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var madrassaBook = new MadrassaBook();
        madrassaBook.ImageUrl = model.ImageUrl;
        madrassaBook.MadrassaClassId = Convert.ToInt16(model.MadrassaClassId);
        madrassaBook.Name = model.Name;
        if (madrassaBook != null)
        {
            await _madrassaBookServices.SaveMadrassaBook(madrassaBook);
            foreach (var item in model.MadrassaBookCatgories)
            {
                if (item.PdfFile == null || item.PdfFile.Length <= 0)
                {
                    return BadRequest("No file or empty file provided.");
                }
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "MadrassaBook");
                var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.PdfFile.FileName);
                var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await item.PdfFile.CopyToAsync(pdfStream);
                }
                var madrassaBookCatgory = new MadrassaBookCatgory()
                {
                    FileNamePDF = pdfFileName,
                    FilePathPDF = pdfFilePath,
                    MadrassaBookId = item.MadrassaBookId,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl
                };
                await _madrassaBookServices.SaveMadrassaBookCatgoryImages(madrassaBookCatgory);

            }

        }
        return Ok(new { Success = true, Message = CustomMessage.Added });
    }

    [HttpPut("UpdateMadrassaBookCatgory")]
    public async Task<IActionResult> UpdatedBookDetail([FromForm] MadrassaBookSaveDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var madrassaBook = new MadrassaBook();
        madrassaBook.Id = Convert.ToInt16(model.Id);
        madrassaBook.ImageUrl = model.ImageUrl;
        madrassaBook.MadrassaClassId = Convert.ToInt16(model.MadrassaClassId);
        madrassaBook.Name = model.Name; 
        if (madrassaBook != null)
        {
            var detailBookOldData = await _madrassaBookServices.Get(madrassaBook.Id);
            await _madrassaBookServices.UpdateMadrassaBook(detailBookOldData, madrassaBook);
            foreach (var item in model.MadrassaBookCatgories)
            {

                if (item.IsDeleted == true)
                {
                    var bookImages = await _madrassaBookServices.GetMadrassaBookCatgoryById(Convert.ToInt16( item.Id));
                    await _madrassaBookServices.DeleteMadrassaBookCatgoryImagePermently(bookImages);
                }
                if (item.Id == null)
                {
                    var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.PdfFile.FileName);
                    var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                    {
                        await item.PdfFile.CopyToAsync(pdfStream);
                    }
                    var madrassaBookCatgory = new MadrassaBookCatgory()
                    {
                        FileNamePDF = pdfFileName,
                        FilePathPDF = pdfFilePath,
                        MadrassaBookId = item.MadrassaBookId,
                        Name = item.Name,
                        ImageUrl = item.ImageUrl
                    };
                    await _madrassaBookServices.SaveMadrassaBookCatgoryImages(madrassaBookCatgory);
                }
                else
                {
                    var madrassaBookId = await _madrassaBookServices.GetMadrassaBookCatgoryById(Convert.ToInt16(item.Id));
                    if (item.IsDeleted == false)
                    {

                        var updateBookImageData = new MadrassaBookCatgory()
                        {
                            Name = item.Name,
                            ImageUrl = item.ImageUrl, 
                        };
                        await _madrassaBookServices.UpdateMadrassaBookCatgory(madrassaBookId, updateBookImageData);
                    }
                    //var bookImageId = await _madrassaBookServices.GetMadrassaBookCatgoryById(item.Id);

                    //var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    //var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.PdfFile.FileName);
                    //var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
                    //if (!Directory.Exists(uploadsFolder))
                    //{
                    //    Directory.CreateDirectory(uploadsFolder);
                    //}
                    //using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                    //{
                    //    await item.PdfFile.CopyToAsync(pdfStream);
                    //}
                    //var madrassaBookCatgory = new MadrassaBookCatgory()
                    //{
                    //    FileNamePDF = pdfFileName,
                    //    FilePathPDF = pdfFilePath,
                    //    MadrassaBookId = item.MadrassaBookId,
                    //    Name = item.Name,
                    //    ImageUrl = item.ImageUrl
                    //};
                    //await _madrassaBookServices.UpdateMadrassaBookCatgory(bookImageId, madrassaBookCatgory);
                }
            }

        }
        return Ok(new { Success = true, Message = CustomMessage.Added });
    }

    [HttpGet("GetMadrassaBookId/{Id}")]
    public async Task<IActionResult> GetMadrassaBookId(int Id)
    {
        var madrassaBookEntity = await _madrassaBookServices.Get(Id);

        if (madrassaBookEntity == null)
        {
            return NotFound();
        }
        if (madrassaBookEntity != null)
        {
            return Ok(new { Data = madrassaBookEntity, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        }
    }

}
