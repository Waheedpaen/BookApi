using Azure;
using Azure.Core;
using EntitiesClasses.Entities;
using HelperDatas.PaginationsClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels.BookDetails;
using static System.Net.Mime.MediaTypeNames;

namespace BookApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookDetailController : ControllerBase
{

    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IBookDetailServices _bookDetailServices;
    public BookDetailController(IBookDetailServices bookDetailServices,IMapper mapper, IConfiguration configuration, IWebHostEnvironment HostEnvironment)
    {
        _configuration = configuration; _mapper = mapper;
        _hostEnvironment = HostEnvironment;
         _bookDetailServices = bookDetailServices;  
    }




    [HttpPost("SaveBookDetail")]
    public async Task<IActionResult> SaveBookDetail([FromForm] BookDetailSaveDto model)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var bookDetail = new BookDetail();
        bookDetail.ImageUrl = model.ImageUrl;
        bookDetail.ScholarId = model.ScholarId;
        bookDetail.Name = model.Name;
        if (bookDetail != null)
        {
            await _bookDetailServices.SaveBookDetail(bookDetail);
            foreach (var item in model.BookImages)
            {
                if (item.PdfFile == null || item.PdfFile.Length <= 0  )
                {
                    return BadRequest("No file or empty file provided.");
                }
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
                var imageDetail = new BookImage()
                {
                    FileNamePDF = pdfFileName,
                    FilePathPDF = pdfFilePath,
                    BookDetailId = item.BookDetailId,
                    Name = item.Name,
                    Image  = item.Image,
                };
                await _bookDetailServices.SaveBookImages(imageDetail);
                
            }
           
        }
        return Ok(new { Success = true, Message = CustomMessage.Added });
    }



    [HttpGet("DownloadBookPdf/{Id}")]
    public async Task<IActionResult> GetPdfById(int Id)
    {
        var pdfDocument = await _bookDetailServices.GetBookImageById(Id);

        if (pdfDocument == null)
        {
            return NotFound();
        }

        var fileStream = new FileStream(pdfDocument.FilePathPDF, FileMode.Open, FileAccess.Read);

        return File(fileStream, "application/pdf", pdfDocument.FileNamePDF);
    }

    [HttpGet("GetImageDetailId/{Id}")]
    public async Task<IActionResult> GetImageDetailId(int Id)
    {
        var bookDetailEntity = await _bookDetailServices.Get(Id);

        if (bookDetailEntity == null)
        {
            return NotFound(); 
        }
        if (bookDetailEntity != null)
        {
            return Ok(new { Data = bookDetailEntity, Success = true, });
        }
        else
        {
            return Ok(new { Data = string.Empty, Success = false, });
        } 
    }


    [HttpDelete("DeleteBookDetail/{Id}")]
    public async Task<IActionResult> DeleteBookDetail(int Id)
    { 
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await  _bookDetailServices.Get(Id);
        if (obj != null)
        {
            await _bookDetailServices.DeleteBookDetail(obj);
            foreach (var item in obj.BookImages)
            {
                await _bookDetailServices.DeleteBookImage(item);
            }
            return Ok(new { Success = true, Message = CustomMessage.Deleted });
        }
        else
        {
            return Ok(new { Message = CustomMessage.RecordNotFound, Success = false, });
        }
    }





    [HttpPut("UpdatedBookDetail")]
    public async Task<IActionResult> UpdatedBookDetail([FromForm] BookDetailSaveDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var bookDetail = new BookDetail();
        bookDetail.Id = Convert.ToInt32(model.Id); 
        bookDetail.ImageUrl = model.ImageUrl;
        bookDetail.ScholarId = model.ScholarId;
        bookDetail.Name = model.Name;
        if (bookDetail != null)
        {
            var detailBookOldData = await _bookDetailServices.Get(bookDetail.Id);
            await _bookDetailServices.UpdateBookDetail(detailBookOldData, bookDetail);
            foreach (var item in model.BookImages)
            {

                if(item.IsDeleted == true) {
                    var bookImages = await _bookDetailServices.GetBookImageById(item.Id);
                    await _bookDetailServices.DeleteBookImage(bookImages);
                }
                if (item.IsSaved == true)
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
                    var imageDetail = new BookImage()
                    {
                        FileNamePDF = pdfFileName,
                        FilePathPDF = pdfFilePath,
                        BookDetailId = item.BookDetailId,
                        Name = item.Name,
                        Image = item.Image,
                    };
                    await _bookDetailServices.SaveBookImages(imageDetail);
                }
                else
                {
                    var bookImageId = await _bookDetailServices.GetBookImageById(item.Id);
                     
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
                    var updateBookImage = new BookImage()
                    {
                        FileNamePDF = pdfFileName,
                        FilePathPDF = pdfFilePath,
                        BookDetailId = item.BookDetailId,
                        Name = item.Name,
                        Image = item.Image,
                    };
                    await _bookDetailServices.UpdateBookImages(bookImageId, updateBookImage);
                }
            }

        }
        return Ok(new { Success = true, Message = CustomMessage.Added });
    }



    [HttpGet("search")]
    public async Task<IActionResult> SearchAndPaginateCategories([FromQuery] SearchAndPaginateOptions options)
    {
        var pagedResult = await _bookDetailServices.SearchAndPaginateAsync(options);
        return Ok(pagedResult);
    }




























}
