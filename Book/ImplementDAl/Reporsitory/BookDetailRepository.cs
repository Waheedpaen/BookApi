

using Microsoft.EntityFrameworkCore;

namespace ImplementDAl.Reporsitory;

public class BookDetailRepository :Reporsitory<BookDetail, int>, IBookDetailRepository
{
    public BookDetailRepository(DataContexts context) : base(context)
    {

    }
    public DataContexts DataContexts => Context as DataContexts;

    public async Task<BookDetail> BookDetailAlreadyExit(string name)
    {
        return await Context.Set<BookDetail>().FirstOrDefaultAsync(data => data.Name == name) ;
    }

    public async Task<BookImage> GetImageId(int? Id)
    {
        return await Context.Set<BookImage>().FirstOrDefaultAsync(obj => obj.Id == Id);

    }

    public async Task<BookDetail> Get(int Id)
    {
         return await Context.Set<BookDetail>().Include(data=>data.Scholar)
            .Include(data => data.Scholar.FarqaCategory)
            .Include(data => data.Scholar.FarqaCategory.BookCategory)
            .Include(data=>data.BookImages).FirstOrDefaultAsync(obj=>obj.Id==Id);
    }

    public async Task<List<BookDetail>> GetBookDetailByScholar(int Id)
    {
        return await Context.Set<BookDetail>().Where(data=>data.ScholarId == Id).ToListAsync();
    }

    public async  Task<BookImage> SaveBookImages(BookImage model)
    {
        model.Updated_At = null;
       await   Context.Set<BookImage>().AddAsync(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<BookImage> DeleteImage(BookImage model)
    {
      //  Context.Set<BookImage>().Where(p => p.Id == model.Id).ExecuteDeleteAsync();
         Context.Set<BookImage>().Remove(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<List<BookImage>> GetBookImagesByBookDetails(int Id)
    {
        return await Context.Set<BookImage>().Where(data => data.BookDetailId == Id).ToListAsync();
    }
}
