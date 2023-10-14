using ImplementDAL.Reporsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory
{
    public  class ScholarRepository : Reporsitory<Scholar, int>, IScholarRepository
    {
        public ScholarRepository(DataContexts context) : base(context)
        {

        }
        public DataContexts DataContexts => Context as DataContexts;

      

        public async Task<Scholar> ScholarNameAlreadyExit(string name)
        {
            return await DataContexts.Set<Scholar>().FirstOrDefaultAsync(data => data.Name == name);
        }

        public async Task<Scholar> Get(int Id)
        {
            return await DataContexts.Set<Scholar>().Include(a => a.FarqaCategory).FirstOrDefaultAsync(data => data.Id == Id);

        }

        public async Task<List<Scholar>> GetScholarByFarqaCategories(int Id)
        {
            return await Context.Set<Scholar>().Where(data => data.FarqaCategoryId == Id).ToListAsync();
        }
    }
}
