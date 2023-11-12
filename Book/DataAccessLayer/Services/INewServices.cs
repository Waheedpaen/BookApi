using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services;

public  interface INewServices
{
    Task<News> Get(int id);
    Task<News> Create(News model);
    Task<News> Update(News update, News model);
    Task<News> Delete(News model);

    public Task<News> NewsAlreadyExit(string name);
    Task<PagedResult<News>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
}
