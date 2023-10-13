 

namespace ImplementDAl.Reporsitory;

public class AudioScholarsRepository : Reporsitory<AudioScholars, int>, IAudioScholarsRepository
{
    public AudioScholarsRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<AudioScholars> AudioScholarsAlreadyExit(string name)
    {
        return await DataContexts.Set<AudioScholars>().FirstOrDefaultAsync(data => data.Name == name);
    }
}
