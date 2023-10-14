using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;

public  interface IMonthlyMagzinesRepository : IRepository<MonthlyMagzine, int>
{

    public Task<MonthlyMagzine> MonthlyMagzinesAlreadyExit(string name);
    Task<MonthlyMagzine> DeleteMonthlyMagzine(MonthlyMagzine model);
}
