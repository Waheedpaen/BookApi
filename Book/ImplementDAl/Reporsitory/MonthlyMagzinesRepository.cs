using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory;

public  class MonthlyMagzinesRepository : Reporsitory<MonthlyMagzine, int>, IMonthlyMagzinesRepository
{
    public MonthlyMagzinesRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<MonthlyMagzine> DeleteMonthlyMagzine(MonthlyMagzine model)
    {
        Context.Set<MonthlyMagzine>().Remove(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<MonthlyMagzine> MonthlyMagzinesAlreadyExit(string name)
    {
        return await DataContexts.Set<MonthlyMagzine>().FirstOrDefaultAsync(data => data.Name == name);

    }
}
