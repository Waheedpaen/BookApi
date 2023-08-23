
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.IUnitofWork;
    public  interface IUnitofWork  :  IDisposable
{
    IUserRepository IUserRepository { get; }
    IScholarRepository IScholarRepository { get; }
    ILookUpRepository  ILookUpRepository { get; }
    IBookCategoryRepository IBookCategory { get; }
    IFarqaCategoryRepository IFarqaCategoryRepository { get; }
    Task<int> CommitAsync();
    public void saveData();
}