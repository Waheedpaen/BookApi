
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.IUnitofWork;
    public  interface IUnitofWork  :  IDisposable
{
    IUserRepository IUserRepository { get; }
    ILookUpRepository  ILookUpRepository { get; }
    IBookCategoryRepository IBookCategory { get; }
    Task<int> CommitAsync();
    public void saveData();
}