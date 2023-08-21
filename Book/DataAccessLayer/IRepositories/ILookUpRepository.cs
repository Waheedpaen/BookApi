using EntitiesClasses.DataContext;
using EntitiesClasses.Entities;
using HelperData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;
 
    public interface ILookUpRepository
    {
        Task<List<BookCategory>> BookCategories();
    }
  
 
 
