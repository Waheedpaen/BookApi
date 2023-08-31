using ImplementDAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory;

    public  class FarqaCategoryRepository : Reporsitory<FarqaCategory, int>, IFarqaCategoryRepository
    {
        public FarqaCategoryRepository(DataContexts context) : base(context)
        {

        }
        public DataContexts DataContexts => Context as DataContexts;

        public async Task<BookCategory> BookCategoryAlreadyExit(string name)
        {
            return await DataContexts.Set<BookCategory>().FirstOrDefaultAsync(data => data.Name == name);
        }

        public async Task<FarqaCategory> FarqaCategoryAlreadyExit(string name)
        {
            return await DataContexts.Set<FarqaCategory>().FirstOrDefaultAsync(data => data.Name == name);
        }

        public async Task<FarqaCategory> Get(int Id)
        {
        return await Context.Set<FarqaCategory>().Include(a=>a.BookCategory).Where(data => data.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<FarqaCategory>> GetFarqaCategoriesByBook(int Id)
        {
             return await  Context.Set<FarqaCategory>().Where(data=>data.BookCategoryId == Id).ToListAsync();
        }

    public Task<List<FarqaCategory>> GetFarqaCategoriesByBook(string name)
    {
        throw new NotImplementedException();
    }
}

