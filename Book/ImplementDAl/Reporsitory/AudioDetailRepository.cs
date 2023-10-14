
namespace ImplementDAl.Reporsitory;

public class AudioDetailRepository : Reporsitory<AudioDetail, int>, IAudioDetailRepository
{
    public AudioDetailRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<AudioDetail> AudioDetailAlreadyExit(string name)
    {
        return await Context.Set<AudioDetail>().FirstOrDefaultAsync(data => data.Name == name);
    }

    public async Task<AudioDetail> Delete(AudioDetail model)
    {
        Context.Set<AudioDetail>().Remove(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<AudioDetail> Get(int Id)
    {
        return await Context.Set<AudioDetail>().Include(a => a.AudioScholars).Where(data => data.Id == Id).FirstOrDefaultAsync();
    }

    public async Task<List<AudioDetail>> GetAudioDetailByAudioScholar(int Id)
    {
        return await Context.Set<AudioDetail>().Where(data => data.AudioScholarsId == Id).ToListAsync();
    }
}