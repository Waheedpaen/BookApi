

using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;

namespace DataAccessLayer.Services;

public  interface IAudioScholarsServices
{
    Task<AudioScholars> Get(int id);
    Task<AudioScholars> Create(AudioScholars model);
    Task<AudioScholars> Update(AudioScholars update, AudioScholars model);
    Task<AudioScholars> Delete(AudioScholars model); 
    public Task<AudioScholars> AudioScholarsAlreadyExit(string name);
    Task<PagedResult<AudioScholars>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
}
