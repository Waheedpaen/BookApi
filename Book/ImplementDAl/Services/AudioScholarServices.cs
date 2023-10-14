using EntitiesClasses.Entities;
using HelperDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Services;

public class AudioScholarServices : IAudioScholarsServices
{
    private readonly IUnitofWork _unitOfWork;

    public AudioScholarServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<AudioScholars> AudioScholarsAlreadyExit(string name)
    {
        return await _unitOfWork.IAudioScholarsRepository.AudioScholarsAlreadyExit(name);
    }

    public async Task<AudioScholars> Create(AudioScholars model)
    {
        model.Updated_At = null;
        await _unitOfWork.IAudioScholarsRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<AudioScholars> Delete(AudioScholars model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<AudioScholars> Get(int id)
    {
        return await _unitOfWork.IAudioScholarsRepository.GetByIdAsync(id);
    }

  



    public async Task<PagedResult<AudioScholars>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<AudioScholars, bool>> predicate = category =>
     string.IsNullOrEmpty(options.SearchTerm) ||
     category.Name.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.IAudioScholarsRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    public async Task<AudioScholars> Update(AudioScholars update, AudioScholars model)
    {
        update.Name = model.Name; 
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }
}





