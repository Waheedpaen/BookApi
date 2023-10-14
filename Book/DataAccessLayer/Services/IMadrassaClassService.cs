using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IMadrassaClassService
    {
        Task<MadrassaClass> Get(int id);
        Task<MadrassaClass> Create(MadrassaClass model);
        Task<MadrassaClass> Update(MadrassaClass update, MadrassaClass model);
        Task<MadrassaClass> Delete(MadrassaClass model); 
        public Task<MadrassaClass> MadrassaBooklAlreadyExit(string name);
        Task<PagedResult<MadrassaClass>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
    }
}
