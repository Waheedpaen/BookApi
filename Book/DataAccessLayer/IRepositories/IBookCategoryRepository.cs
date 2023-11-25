using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;

public interface IBookCategoryRepository : IRepository<BookCategory, int>
{
    public Task<BookCategory> BookCategoryAlreadyExit(string name);
    Task<List<BookCategory>> GetSuggestions(string searchword);
}

    
 
