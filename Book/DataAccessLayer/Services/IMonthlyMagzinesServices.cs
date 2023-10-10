using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services;

public  interface IMonthlyMagzinesServices 
{
    Task<MonthlyMagzine> Get(int id); 
    Task<MonthlyMagzine> Create(MonthlyMagzine model);
    Task<MonthlyMagzine> Update(MonthlyMagzine update, MonthlyMagzine model);
    Task<MonthlyMagzine> UpdateForwithoutPdf(MonthlyMagzine update, MonthlyMagzine model);
    Task<MonthlyMagzine> Delete(MonthlyMagzine model);
    //Task<List<BookCategory>> GetBookCategory(SeachItem searchitem);
     Task<MonthlyMagzine> MonthlyMagzinesAlreadyExit(string name);
    Task<PagedResult<MonthlyMagzine>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
}
