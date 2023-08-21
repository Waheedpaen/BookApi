using Azure;
using DataAccessLayer.Services;
using HelperData;
using ImplementDAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModels.UserViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILookUpServices  _lookUpServices;
        private readonly IConfiguration _config;
        public LookUpController(ILookUpServices lookUpServices, IMapper mapper, IConfiguration config)
        {
            _lookUpServices = lookUpServices;
            _mapper = mapper;
            _config = config;
        }
        [HttpGet("BookCategories")]
        public async Task<IActionResult> BookCategories()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var enityData = await _lookUpServices.BookCategories();

            if(enityData != null)
            {
                return Ok(new
                {
                    data = enityData,
                    Success = true,
                });
            }
            else
            {
                return Ok(new
                {
                    data = string.Empty,
                    Success = true,
                });
            }
           
            

        }
    }
}
