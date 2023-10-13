using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;

public interface IAudioDetailRepository : IRepository<AudioDetail, int>
{
    public Task<AudioDetail> Get(int Id);
    public Task<AudioDetail> AudioDetailAlreadyExit(string name);
    Task<List<AudioDetail>> GetAudioDetailByAudioScholar(int Id);
    Task<AudioDetail> Delete(AudioDetail model);
}

