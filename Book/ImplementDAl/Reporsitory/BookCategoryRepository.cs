using ImplementDAL.Reporsitory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ImplementDAl.Reporsitory;

public class BookCategoryRepository : Reporsitory<BookCategory, int>, IBookCategoryRepository
{
    public BookCategoryRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<BookCategory> BookCategoryAlreadyExit(string name)
    {
    return await DataContexts.Set<BookCategory>().FirstOrDefaultAsync(data => data.Name == name); 
    }

    public async Task<List<BookCategory>> GetSuggestions(string searchword)
    {
   var matchingSuggestions = await DataContexts.Set<BookCategory>().Where(s => s.Name.ToLower().Contains(searchword.ToLower())).ToListAsync();

        return matchingSuggestions;
    }
}
