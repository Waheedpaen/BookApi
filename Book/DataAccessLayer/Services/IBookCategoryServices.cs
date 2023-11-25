using EntitiesClasses.CommonClasses;
using EntitiesClasses.Entities;
using HelperDatas;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IBookCategoryServices
    {
        Task<BookCategory> Get(int id);
        Task<BookCategory> Create(BookCategory model);
        Task<BookCategory> Update(BookCategory update, BookCategory model); 
        Task<BookCategory> Delete(BookCategory model);
         
        public Task<BookCategory> BookCategoryAlreadyExit(string name);
        Task<PagedResult<BookCategory>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
        Task<List<BookCategory>> GetSuggestions(string searchword);
    }



}
 
 
