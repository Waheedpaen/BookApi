using Azure.Core;
using EntitiesClasses.Entities;
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




    [HttpPost("SaveFarqaCategory")]
    public async Task<IActionResult> SaveBookCategory([FromBody] BookDetailSaveDto model)
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















































}
