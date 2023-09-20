

using DataAccessLayer.IUnitofWork;
using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;

namespace DataAccessLayer.Services;
 
    public interface IBookDetailServices  
    {
    #region BookDetailCrud
    public Task<BookDetail> Get(int Id);
    public Task<BookDetail> BookDetailAlreadyExit(string name);

    Task<BookDetail> SaveBookDetail(BookDetail model);

    Task<BookDetail> DeleteBookDetail(BookDetail model);
    Task<BookDetail> UpdateBookDetail(BookDetail update, BookDetail model);
    Task<PagedResult<BookDetail>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
    Task<List<BookImage>> GetBookImagesByBookDetails(int Id);
    #endregion

    #region BookImagesCrud
    Task<BookImage> DeleteBookImage(BookImage model);
    Task<BookImage> GetBookImageById(int? Id);
    Task<BookImage> SaveBookImages(BookImage model);
    Task<BookImage> CreateBookImages(BookImage model);
    Task<BookImage> UpdateBookImages(BookImage update, BookImage model);

    Task<BookImage> DeleteBookImages(BookImage model);
    Task<List<BookDetail>> GetBookDetailByScholar(int Id);
    Task<BookImage> UpdateBookImagesForFile(BookImage update, BookImage model);
    #endregion





}
 
