

using Microsoft.Extensions.Options;

namespace ImplementDAl.Services;

public class NewServices : INewServices
{
    private readonly IUnitofWork _unitOfWork;

    public NewServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<News> Create(News model)
    {
        model.Updated_At = null;
        await _unitOfWork.INewsRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<News> Delete(News model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }


    public async   Task<News> Get(int id)
    {
        return await _unitOfWork.INewsRepository.GetByIdAsync(id);
    }
    public async Task<News> NewsAlreadyExit(string name)
    {
        return await _unitOfWork.INewsRepository.NewsAlreadyExit(name);
    }

    public async Task<PagedResult<News>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<News, bool>> predicate = category =>
        string.IsNullOrEmpty(options.SearchTerm) ||
        category.Header.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.INewsRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    

    public async  Task<News> Update(News update, News model)
    {
        update.Header = model.Header;
        update.Description = model.Description;
        update.ImageUrl = model.ImageUrl;
        update.Title = model.Title; 
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }
}
