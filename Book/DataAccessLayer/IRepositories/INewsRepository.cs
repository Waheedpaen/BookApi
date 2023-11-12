

using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;

namespace DataAccessLayer.IRepositories;

public  interface INewsRepository : IRepository<News, int>
{
    Task<News> NewsAlreadyExit(string header);
}
