


using DataAccessLayer.IUnitofWork;
using DataAccessLayer.Services;
using ImplementDAl.Services;
using ImplementDAL.Services;
using ImplementDAL.UnitWorks;
using ViewModel.AutoMapper;

namespace MobileManagementSystem.Extension
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection Services)
        {
            //Services.AddTransient<IBrandService, BrandService>();
            //Services.AddTransient<IMobileService, MobileService>();
             Services.AddTransient<IScholarServices, ScholarServices>();
            Services.AddScoped<IUnitofWork, UnitWork>();
            Services.AddTransient<IMadrassaBookServices, MadrassaBookServices>();
            Services.AddTransient<IMadrassaClassService, MadrassaClassService>(); 
            Services.AddTransient<IUserService, UserService>();
            Services.AddTransient<ILookUpServices, LookUpServices>();
            Services.AddTransient<IBookCategoryServices, BookCategoryServices>();
            Services.AddTransient<IFarqaCategoryServices, FarqaCategoryServices>();
            Services.AddTransient<IBookDetailServices, BookDetailServices>();
            //Services.AddTransient<IOperatingSystemService, OperatingSystemService>();
            //Services.AddTransient<ILoggerManager, LoggerManager>();
            Services.AddAutoMapper(typeof(AutoMappers));

            return Services; 
        }
    }
}
