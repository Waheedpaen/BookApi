
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.IUnitofWork;
    public  interface IUnitofWork  :  IDisposable
{
    IUserRepository IUserRepository { get; }
    Task<int> CommitAsync();
    public void saveData();
}