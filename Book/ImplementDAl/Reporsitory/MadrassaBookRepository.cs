

namespace ImplementDAl.Reporsitory;

public class MadrassaBookRepository : Reporsitory<MadrassaBook, int>, IMadrassaBookRepository
{
    public MadrassaBookRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;
     #region MadrassaBook 
    public async Task<MadrassaBook> MadrassaBooklAlreadyExit(string name)
    {
        return await Context.Set<MadrassaBook>().FirstOrDefaultAsync(data => data.Name == name);
    } 
    public async Task<MadrassaBook> Get(int Id)
    {
        return await Context.Set<MadrassaBook>().Include(data => data.MadrassaBookCatgories).FirstOrDefaultAsync(obj => obj.Id == Id);
    }

    #endregion
     

    #region MadrassaBookCatgory 
    public async Task<MadrassaBookCatgory> GetMadrassaBookCatgoryId(int Id)
    {
        return await Context.Set<MadrassaBookCatgory>().FirstOrDefaultAsync(obj => obj.Id == Id);
    }

    public async Task<MadrassaBookCatgory> SaveMadrassaBookCatgory(MadrassaBookCatgory model)
    {
        model.Updated_At = null;
        await Context.Set<MadrassaBookCatgory>().AddAsync(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<MadrassaBookCatgory> DeleteMadrassaBookCatgory(MadrassaBookCatgory model)
    {
        Context.Set<MadrassaBookCatgory>().Remove(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<List<MadrassaBookCatgory>> MadrassaBookCatgorybyMadrassBook(int Id)
    {
         return await Context.Set<MadrassaBookCatgory>().Where(data=>data.MadrassaBookId == Id).ToListAsync();
    }


    #endregion






}
