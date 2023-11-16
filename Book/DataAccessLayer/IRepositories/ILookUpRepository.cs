using EntitiesClasses.DataContext;
using EntitiesClasses.Entities;
using HelperData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;
 
    public interface ILookUpRepository
    {
     Task<List<AudioScholars>> AudioScholars();
    Task<List<AudioDetail>> AudioDetails();
    Task<List<BookCategory>> BookCategories();
    Task<List<FarqaCategory>> FarqaCategories();
     Task<List<Scholar>> GetScholars();
    Task<List<BookDetail>> GetBookDetails();

    Task<List<BookImage>> GetBookImages();
    Task<List<MadrassaBook>> GetMadrassaBooks();
    Task<List<MonthlyMagzine>> MonthlyMagzines();
    Task<List<News>> News();

}



