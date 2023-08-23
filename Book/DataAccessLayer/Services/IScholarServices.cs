﻿
using EntitiesClasses.Entities;

namespace DataAccessLayer.Services;
 
    public interface IScholarServices
    {
    Task<Scholar> Get(int id);
    Task<Scholar> Create(Scholar model);
    Task<Scholar> Update(Scholar update, Scholar model);
    public Task<Scholar> ScholarNameAlreadyExit(string name);
    Task<Scholar> Delete(Scholar model);
    public Task<List<Scholar>> GetScholarByFarqaCategories(int Id);
}
 
