using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface ILookUpServices
    {
        Task<List<BookCategory>> BookCategories();
        Task<List<FarqaCategory>> FarqaCategories();
        Task<List<Scholar>> GetScholars();
    }
}
