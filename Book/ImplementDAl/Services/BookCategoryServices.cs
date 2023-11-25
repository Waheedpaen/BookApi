

namespace ImplementDAl.Services;


public  class BookCategoryServices : IBookCategoryServices
{
    private readonly IUnitofWork _unitOfWork;

    public BookCategoryServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<BookCategory> BookCategoryAlreadyExit(string name)
    {
         return await _unitOfWork.IBookCategory.BookCategoryAlreadyExit(name);
    }

    public async Task<BookCategory> Create(BookCategory model)
    {
        model.Updated_At = null;
        await _unitOfWork.IBookCategory.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<BookCategory> Delete(BookCategory model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<BookCategory> Get(int id)
    {
        return await _unitOfWork.IBookCategory.GetByIdAsync(id);
    }

    public async Task<List<BookCategory>> GetSuggestions(string searchword)
    {
         return await _unitOfWork.IBookCategory.GetSuggestions(searchword);
    }

    public async Task<PagedResult<BookCategory>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<BookCategory, bool>> predicate = category =>
     string.IsNullOrEmpty(options.SearchTerm) ||
     category.Name.Contains(options.SearchTerm) ;

        var pagedResult = await _unitOfWork.IBookCategory.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    public async Task<BookCategory> Update(BookCategory update, BookCategory model)
    {
        update.Name = model.Name;
        update.Description = model.Description;
        update.ImageUrl = model.ImageUrl;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }
}
