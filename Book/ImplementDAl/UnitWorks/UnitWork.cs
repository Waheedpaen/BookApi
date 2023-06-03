
using DataAccessLayer.IRepositories;
using DataAccessLayer.IUnitofWork;
using EntitiesClasses.DataContext;
using ImplementDAL.Reporsitory;

namespace ImplementDAL.UnitWorks;
 
     public  class UnitWork : IUnitofWork
    {
    public readonly DataContexts _context;
    public UnitWork(DataContexts context)
    {
        _context = context;
    } 
    private UserRepository _userRepository; 

       
    public IUserRepository IUserRepository => _userRepository ?? new(_context);
     
    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void saveData()
    {

    }
}
 