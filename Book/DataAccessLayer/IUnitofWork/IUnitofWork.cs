
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.IUnitofWork;
    public  interface IUnitofWork  :  IDisposable
{
    IUserRepository IUserRepository { get; }
    IScholarRepository IScholarRepository { get; }

    IMonthlyMagzinesRepository IMonthlyMagzinesRepository { get; }
    ILookUpRepository  ILookUpRepository { get; }
    IMadrassaBookRepository IMadrassaBookRepository { get; } 
    IBookCategoryRepository IBookCategory { get; }
    IFarqaCategoryRepository IFarqaCategoryRepository { get; }
    IBookDetailRepository IBookDetailRepository { get; }

    IMadrassaClassRepository IMadrassaClassRepository { get; }
    Task<int> CommitAsync();
    public void saveData();
}