


namespace ImplementDAl.Services;

public class MadrassaBookServices : IMadrassaBookServices
{
    private readonly IUnitofWork _unitOfWork;

    public MadrassaBookServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }
    public  Task<MadrassaBookCatgory> CreateMadrassaBookCatgoryImages(MadrassaBookCatgory model)
    {
        throw new NotImplementedException();
    }

    public async Task<MadrassaBook> DeleteMadrassaBook(MadrassaBook model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public Task<MadrassaBookCatgory> DeleteMadrassaBookCatgoryImagePermently(MadrassaBookCatgory model)
    {
        return _unitOfWork.IMadrassaBookRepository.DeleteMadrassaBookCatgory(model);
    }

    public async Task<MadrassaBookCatgory> DeleteMadrassaBookCatgoryImages(MadrassaBookCatgory model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<MadrassaBook> Get(int Id)
    {
        return await _unitOfWork.IMadrassaBookRepository.Get(Id);
    }

    public async Task<MadrassaBookCatgory> GetMadrassaBookCatgoryById(int Id)
    {
        return await _unitOfWork.IMadrassaBookRepository.GetMadrassaBookCatgoryId(Id); 
    }

    public  async Task<MadrassaBook> MadrassaBookAlreadyExit(string name)
    {
         return await _unitOfWork.IMadrassaBookRepository.MadrassaBooklAlreadyExit(name);
    }

    public async Task<List<MadrassaBookCatgory>> MadrassaBookCatgorybyMadrassBook(int Id)
    {
        return await _unitOfWork.IMadrassaBookRepository.MadrassaBookCatgorybyMadrassBook(Id);
    }

    public async Task<MadrassaBook> SaveMadrassaBook(MadrassaBook model)
    {
        model.Updated_At = null;
        await _unitOfWork.IMadrassaBookRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public  async Task<MadrassaBookCatgory> SaveMadrassaBookCatgoryImages(MadrassaBookCatgory model)
    {
         return await _unitOfWork.IMadrassaBookRepository.SaveMadrassaBookCatgory(model);
    }

    public async Task<PagedResult<MadrassaBook>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<MadrassaBook, bool>> predicate = category =>
       category.Name.Contains(options.SearchTerm);
        var pagedResult = await _unitOfWork.IMadrassaBookRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

    public  async Task<MadrassaBookCatgory> UpdateMadrassaBookCatgory(MadrassaBookCatgory update, MadrassaBookCatgory model)
    {
        update.MadrassaBookId = model.MadrassaBookId;
        update.Name = model.Name;
        update.ImageUrl = model.ImageUrl;
        update.FileNamePDF = model.FileNamePDF;
        update.Updated_At = model.Updated_At;
        update.FilePathPDF = model.FilePathPDF;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<MadrassaBook> UpdateMadrassaBook(MadrassaBook update, MadrassaBook model)
    {
        update.Updated_At = model.Updated_At;
        update .Name = model.Name; 
        await _unitOfWork.CommitAsync();
        return update;
    }
}
