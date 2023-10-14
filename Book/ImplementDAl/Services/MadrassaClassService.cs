using HelperDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Services;

public class MadrassaClassService   : IMadrassaClassService
{
    private readonly IUnitofWork _unitOfWork;

    public MadrassaClassService(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<MadrassaClass> MadrassaBooklAlreadyExit(string name)
    {
        return await _unitOfWork.IMadrassaClassRepository.MadrassaClassAlreadyExit(name);
    }

    public async Task<MadrassaClass> Create(MadrassaClass model)
    {
        model.Updated_At = null;
        await _unitOfWork.IMadrassaClassRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<MadrassaClass> Delete(MadrassaClass model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<MadrassaClass> Get(int id)
    {
        return await _unitOfWork.IMadrassaClassRepository.GetByIdAsync(id);
    }

 


    public async Task<PagedResult<MadrassaClass>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<MadrassaClass, bool>> predicate = category =>
     string.IsNullOrEmpty(options.SearchTerm) ||
     category.Name.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.IMadrassaClassRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    public async Task<MadrassaClass> Update(MadrassaClass update, MadrassaClass model)
    {
        update.Name = model.Name;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }
}

 
