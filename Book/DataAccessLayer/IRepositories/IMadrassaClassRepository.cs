using EntitiesClasses.DataContext;
using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;

public  interface IMadrassaClassRepository : IRepository<MadrassaClass, int>
{
    Task<MadrassaClass> MadrassaClassAlreadyExit(string name);
   
}