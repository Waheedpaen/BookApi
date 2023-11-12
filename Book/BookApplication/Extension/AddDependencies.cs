


using DataAccessLayer.IUnitofWork;
using DataAccessLayer.Services;
using ImplementDAl.Services;
using ImplementDAL.Services;
using ImplementDAL.UnitWorks;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            Services.AddTransient<IMonthlyMagzinesServices, MonthlyMagzinesServices>();
            Services.AddTransient<IUserService, UserService>(); 
            Services.AddTransient<IAudioDetailServices, AudioDetailServices>();
            Services.AddTransient<IAudioScholarsServices,  AudioScholarServices>(); 
            Services.AddTransient<ILookUpServices, LookUpServices>(); 
            Services.AddTransient<IBookCategoryServices, BookCategoryServices>();
            Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();   
            Services.AddTransient<IFarqaCategoryServices, FarqaCategoryServices>();
            Services.AddTransient<IBookDetailServices, BookDetailServices>();
            Services.AddTransient<IChatServices, ChatServices>();
            Services.AddTransient<INewServices, NewServices>();  
            //Services.AddTransient<IOperatingSystemService, OperatingSystemService>();
            //Services.AddTransient<ILoggerManager, LoggerManager>();
            Services.AddAutoMapper(typeof(AutoMappers));
            Services.AddSignalR();
            return Services; 
        }
    }
}
