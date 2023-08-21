
using DataAccessLayer.IRepositories;
using DataAccessLayer.IUnitofWork;
using EntitiesClasses.DataContext;
using ImplementDAl.Reporsitory;
using ImplementDAL.Reporsitory;

namespace ImplementDAL.UnitWorks;
 
     public  class UnitWork : IUnitofWork
    {
    private UserRepository _userRepository;
    private LookUpRepository _lookUpRepository;
    private BookCategoryRepository _bookCategoryRepository;
    public readonly DataContexts _context;
    public UnitWork(DataContexts context)
    {
        _context = context;
    } 
  

    public IUserRepository IUserRepository => _userRepository ?? new(_context); 
    public ILookUpRepository ILookUpRepository => _lookUpRepository ?? new(_context);

    public IBookCategoryRepository IBookCategory => _bookCategoryRepository ?? new(_context);

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
 