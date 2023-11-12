
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.IUnitofWork;
    public  interface IUnitofWork  :  IDisposable
{
    IUserRepository IUserRepository { get; }
    IScholarRepository IScholarRepository { get; }
    IChatRepository IChatRepository { get; }
    IMonthlyMagzinesRepository IMonthlyMagzinesRepository { get; }
    ILookUpRepository  ILookUpRepository { get; }
    INewsRepository INewsRepository { get; }
    IMadrassaBookRepository IMadrassaBookRepository { get; }
    IAudioScholarsRepository IAudioScholarsRepository { get; }
    IAudioDetailRepository IAudioDetailRepository { get; }
    IBookCategoryRepository IBookCategory { get; }
    IFarqaCategoryRepository IFarqaCategoryRepository { get; }                                                                                                                                                                                                                                              
    IBookDetailRepository IBookDetailRepository { get; }

    IMadrassaClassRepository IMadrassaClassRepository { get; }
    Task<int> CommitAsync();
    public void saveData();
}