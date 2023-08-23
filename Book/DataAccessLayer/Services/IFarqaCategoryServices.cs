
using EntitiesClasses.Entities;

namespace DataAccessLayer.Services;

    public  interface IFarqaCategoryServices
    {
    Task<FarqaCategory> Get(int id);
    Task<FarqaCategory> Create(FarqaCategory model);
    Task<FarqaCategory> Update(FarqaCategory update, FarqaCategory model);
    public Task<FarqaCategory> FarqaCategoryAlreadyExit(string name);
    Task<FarqaCategory> Delete(FarqaCategory model);
    Task<List<FarqaCategory>> GetFarqaCategoriesByBook(int Id);
    }

