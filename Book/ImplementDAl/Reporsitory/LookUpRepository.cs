using HelperData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Reporsitory;
 
    public class LookUpRepository : ILookUpRepository
    {
        protected readonly DbContext Context;
        public LookUpRepository(DbContext context)
        {
            Context = context; 
           
        }

    public async Task<List<BookCategory>> BookCategories()
    {
      return await Context.Set<BookCategory>().ToListAsync();
    }

    public async Task<List<FarqaCategory>> FarqaCategories()
    {
        //return await Context.Set<FarqaCategory>().Include(data => data.BookCategory).ToListAsync();
        return await Context.Set<FarqaCategory>().ToListAsync();
    }

    public async Task<List<Scholar>> GetScholars()
    {
        return await Context.Set<Scholar>().ToListAsync();
    }
}
 