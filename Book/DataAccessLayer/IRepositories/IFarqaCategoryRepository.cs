using EntitiesClasses.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;
 
    public  interface IFarqaCategoryRepository : IRepository<FarqaCategory, int>
   {
    public Task<FarqaCategory> Get(int Id);
    public Task<FarqaCategory> FarqaCategoryAlreadyExit(string name);
    Task<List<FarqaCategory>> GetFarqaCategoriesByBook(int Id);
}
