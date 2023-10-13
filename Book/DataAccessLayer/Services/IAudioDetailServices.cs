using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services;

public interface IAudioDetailServices
{
    Task<AudioDetail> Get(int id);
    Task<AudioDetail> Create(AudioDetail model);
    Task<AudioDetail> Update(AudioDetail update, AudioDetail model);
    Task<AudioDetail> UpdateForAudioDetail(AudioDetail update, AudioDetail model);
    public Task<AudioDetail> AudioDetailAlreadyExit(string name);
    Task<AudioDetail> Delete(AudioDetail model); 
    Task<List<AudioDetail>> GetAudioDetailByAudioScholar(int Id);
    Task<PagedResult<AudioDetail>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
}
