

using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System.Linq.Expressions;

namespace ImplementDAl.Services;

public class BookDetailServices : IBookDetailServices
{
    private readonly IUnitofWork _unitOfWork;

    public BookDetailServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<BookDetail> BookDetailAlreadyExit(string name)
    {
        return await _unitOfWork.IBookDetailRepository.BookDetailAlreadyExit(name);
    }

    public async Task<BookImage> CreateBookImages(BookImage model)
    {
        return await _unitOfWork.IBookDetailRepository.SaveBookImages(model);
    }
    public async Task<BookImage> DeleteBookImages(BookImage model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<BookDetail> Get(int Id)
    {
        return await _unitOfWork.IBookDetailRepository.Get(Id);
    }
    public async Task<List<BookDetail>> GetBookDetailByScholar(int Id)
    {
        return await _unitOfWork.IBookDetailRepository.GetBookDetailByScholar(Id);
    }

    public async Task<BookDetail> SaveBookDetail(BookDetail model)
    {
        model.Updated_At = null;
        await _unitOfWork.IBookDetailRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }
    public async Task<BookImage> SaveBookImages(BookImage model)
    {
        return await _unitOfWork.IBookDetailRepository.SaveBookImages(model);
    }
    public async Task<BookDetail> DeleteBookDetail(BookDetail model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }
    public async Task<BookImage> UpdateBookImages(BookImage update, BookImage model)
    {
        update.BookDetailId = model.BookDetailId;
        update.Name = model.Name;
        update.Image = model.Image;
        update.FileNamePDF = model.FileNamePDF;
        update.Updated_At = model.Updated_At;
        update.FilePathPDF = model.FilePathPDF;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<BookImage> DeleteBookImage(BookImage model)
    {

      var deleteBookImage=  await _unitOfWork.IBookDetailRepository.DeleteImage(model); 
        return deleteBookImage;
    }

    public async Task<BookImage> GetBookImageById(int? Id)
    {
        return await _unitOfWork.IBookDetailRepository.GetImageId(Id);
    }

    public async Task<BookDetail> UpdateBookDetail(BookDetail update, BookDetail model)
    {
        update.Id = model.Id;
        update.ImageUrl = model.ImageUrl;
        update.Name = model.Name;
        update.ScholarId = model.ScholarId;
        update.Name = model.Name;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<PagedResult<BookDetail>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<BookDetail, bool>> predicate = category =>
         string.IsNullOrEmpty(options.SearchTerm) ||
         category.Name.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.IBookDetailRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    public async Task<BookImage> UpdateBookImagesForFile(BookImage update, BookImage model)
    {
       
        update.Name = model.Name;
        update.Image = model.Image; 
        update.Updated_At = model.Updated_At; 
        await _unitOfWork.CommitAsync();
        return update; 
    }

    public async Task<List<BookImage>> GetBookImagesByBookDetails(int Id)
    {
        return await _unitOfWork.IBookDetailRepository.GetBookImagesByBookDetails(Id);
    }
}
 
