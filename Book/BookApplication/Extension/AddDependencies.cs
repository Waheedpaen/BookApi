


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
            //Services.AddTransient<IOSVService, OSVService>();
            Services.AddScoped<IUnitofWork, UnitWork>();
            //Services.AddTransient<IOderService, OderService>();
            Services.AddTransient<IUserService, UserService>();
            Services.AddTransient<ILookUpServices, LookUpServices>();
            Services.AddTransient<IBookCategoryServices, BookCategoryServices>();
            //Services.AddTransient<IOperatingSystemService, OperatingSystemService>();
            //Services.AddTransient<ILoggerManager, LoggerManager>();
            Services.AddAutoMapper(typeof(AutoMappers));

            return Services;


            return Services;
        }
    }
}
