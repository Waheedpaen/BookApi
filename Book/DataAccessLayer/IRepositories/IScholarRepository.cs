
using EntitiesClasses.Entities;

namespace DataAccessLayer.IRepositories;

    public  interface IScholarRepository : IRepository<Scholar, int>
    {
    public Task<Scholar> Get(int Id);
    public Task<Scholar> ScholarNameAlreadyExit(string name);
    public Task<List<Scholar>> GetScholarByFarqaCategories(int Id);

}

