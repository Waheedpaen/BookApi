using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Services;

public class MonthlyMagzinesServices : IMonthlyMagzinesServices
{
    private readonly IUnitofWork _unitOfWork;

    public MonthlyMagzinesServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<MonthlyMagzine> MonthlyMagzinesAlreadyExit(string name)
    {
        return await _unitOfWork.IMonthlyMagzinesRepository.MonthlyMagzinesAlreadyExit(name);
    }

    public async Task<MonthlyMagzine> Create(MonthlyMagzine model)
    {
        model.Updated_At = null;
        await _unitOfWork.IMonthlyMagzinesRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<MonthlyMagzine> Delete(MonthlyMagzine model)
    {
        return await _unitOfWork.IMonthlyMagzinesRepository.DeleteMonthlyMagzine(model);
    }

    public async Task<MonthlyMagzine> Get(int id)
    {
        return await _unitOfWork.IMonthlyMagzinesRepository.GetByIdAsync(id);
    }

  

    public async Task<PagedResult<MonthlyMagzine>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<MonthlyMagzine, bool>> predicate = category =>
 string.IsNullOrEmpty(options.SearchTerm) ||
 category.Name.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.IMonthlyMagzinesRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    public async Task<MonthlyMagzine> Update(MonthlyMagzine update, MonthlyMagzine model)
    {
        //update.Id = model.Id;
        update.ImageUrl = model.ImageUrl;
        update.FileNamePDF = model.FileNamePDF;
        update.FilePathPDF = model.FilePathPDF;
        update.Description = model.Description;
        update.Name = model.Name;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<MonthlyMagzine> UpdateForwithoutPdf(MonthlyMagzine update, MonthlyMagzine model)
    {
        update.ImageUrl = model.ImageUrl; 
        update.Name = model.Name;
        update.Description = model.Description;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }
}
