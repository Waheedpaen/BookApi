

using EntitiesClasses.Entities;

namespace DataAccessLayer.IRepositories;

public interface IMadrassaBookRepository : IRepository<MadrassaBook, int>
{
     public Task<MadrassaBook> Get(int Id);
    public Task<MadrassaBook> MadrassaBooklAlreadyExit(string name);
    Task<MadrassaBookCatgory> SaveMadrassaBookCatgory(MadrassaBookCatgory model);
 
    Task<MadrassaBookCatgory> GetMadrassaBookCatgoryId(int Id);
    Task<MadrassaBookCatgory> DeleteMadrassaBookCatgory(MadrassaBookCatgory model);
    Task<List<MadrassaBookCatgory>> MadrassaBookCatgorybyMadrassBook(int Id);

}
