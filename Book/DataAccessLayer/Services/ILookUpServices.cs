using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface ILookUpServices
    {
        Task<List<BookCategory>> BookCategories();
        Task<List<AudioScholars>> AudioScholars();
        Task<List<AudioDetail>> AudioDetails();
        Task<List<FarqaCategory>> FarqaCategories();
        Task<List<Scholar>> GetScholars();
        Task<List<BookDetail>> GetBookDetails();  
        Task<List<BookImage>> GetBookImages();
        Task<List<MadrassaBook>> GetMadrassaBooks();
        Task<List<MonthlyMagzine>> GetMonthlyMagzines(); 
        Task<List<News>> News();
    }
}
