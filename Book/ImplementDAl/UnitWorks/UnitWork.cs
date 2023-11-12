
using DataAccessLayer.IRepositories;
using DataAccessLayer.IUnitofWork;
using EntitiesClasses.DataContext;
using ImplementDAl.Reporsitory;
using ImplementDAL.Reporsitory;

namespace ImplementDAL.UnitWorks;
 
     public  class UnitWork : IUnitofWork
    {
    private UserRepository _userRepository;
    private NewsRepository _newsRepository;
    private ChatRepository _chatRepository;
    private LookUpRepository _lookUpRepository;
    private MadrassaClassRepository _madrassaClassRepository;
    private FarqaCategoryRepository _faarqaCategoryRepository;
    private BookCategoryRepository _bookCategoryRepository;
    private MonthlyMagzinesRepository _monthlyMagzinesRepository;
    private AudioScholarsRepository _audioScholarsRepository;
    private ScholarRepository _scholarRepository;
    private BookDetailRepository  _bookDetailRepository;
    private AudioDetailRepository _audioDetailRepository;
    private MadrassaBookRepository _madrassaBookRepository;
    public readonly DataContexts _context;
    public UnitWork(DataContexts context)
    {
        _context = context;
    }  
    public IUserRepository IUserRepository => _userRepository ?? new(_context); 
    public ILookUpRepository ILookUpRepository => _lookUpRepository ?? new(_context); 
    public IBookCategoryRepository IBookCategory => _bookCategoryRepository ?? new(_context); 
    public IFarqaCategoryRepository IFarqaCategoryRepository => _faarqaCategoryRepository ?? new(_context); 
    public IScholarRepository IScholarRepository => _scholarRepository ?? new(_context); 
    public IBookDetailRepository IBookDetailRepository => _bookDetailRepository ?? new(_context); 
    public IMadrassaBookRepository IMadrassaBookRepository => _madrassaBookRepository ?? new(_context);
    
    public IMadrassaClassRepository IMadrassaClassRepository => _madrassaClassRepository ?? new(_context);

    public IMonthlyMagzinesRepository IMonthlyMagzinesRepository => _monthlyMagzinesRepository ?? new(_context);
   
    public IAudioScholarsRepository IAudioScholarsRepository => _audioScholarsRepository ?? new(_context);

    public IAudioDetailRepository IAudioDetailRepository => _audioDetailRepository ?? new(_context);

    public IChatRepository IChatRepository => _chatRepository ?? new(_context);

    public INewsRepository INewsRepository => _newsRepository ?? new(_context);  

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
 