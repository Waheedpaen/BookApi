using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory;

public class MadrassaClassRepository : Reporsitory<MadrassaClass, int>, IMadrassaClassRepository
{
    public MadrassaClassRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;
    public async Task<MadrassaClass> MadrassaClassAlreadyExit(string name)
    {
        return await DataContexts.Set<MadrassaClass>().FirstOrDefaultAsync(data => data.Name == name);

    }

   
}