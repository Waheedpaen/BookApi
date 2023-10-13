using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;
 
    public interface IBookDetailRepository : IRepository<BookDetail, int>
{
    public Task<BookDetail> Get(int Id);
    public Task<BookDetail> BookDetailAlreadyExit(string name);
   
    Task<List<BookDetail>> GetBookDetailByScholar(int Id);
    Task<List<BookImage>> GetBookImagesByBookDetails(int Id);
    Task<BookImage> SaveBookImages(BookImage model);
    Task<BookImage> GetImageId(int? Id);
    Task<BookImage> DeleteImage(BookImage model);
   

}
 
