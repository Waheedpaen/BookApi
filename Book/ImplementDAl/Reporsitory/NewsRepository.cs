using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory;

public class NewsRepository : Reporsitory<News, int>, INewsRepository
{
    public NewsRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<News> NewsAlreadyExit(string header)
    {
        return await DataContexts.Set<News>().FirstOrDefaultAsync(data => data.Header == header && data.IsDeleted == false);
    }
}