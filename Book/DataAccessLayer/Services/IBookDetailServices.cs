

using DataAccessLayer.IUnitofWork;
using EntitiesClasses.Entities;

namespace DataAccessLayer.Services;
 
    public interface IBookDetailServices  
    {
    public Task<BookDetail> Get(int Id);
    public Task<BookDetail> BookDetailAlreadyExit(string name);

    Task<BookDetail> SaveBookDetail(BookDetail model);

     Task<BookDetail> DeleteBookDetail(BookDetail model);
    Task<BookDetail> UpdateBookDetail(BookDetail update, BookDetail model);




    #region

    Task<BookImage> SaveBookImages(BookImage model);
    Task<BookImage> CreateBookImages(BookImage model);
    Task<BookImage> UpdateBookImages(BookImage update, BookImage model);

    Task<BookImage> DeleteBookImages(BookImage model);
    Task<List<BookDetail>> GetBookDetailByScholar(int Id);
    #endregion
    Task<BookImage> DeleteBookImage(BookImage model);
    Task<BookImage> GetBookImageById(int ?Id);




}
 
