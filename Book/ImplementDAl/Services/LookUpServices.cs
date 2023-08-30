

namespace ImplementDAl.Services;

public class LookUpServices : ILookUpServices

{
    private readonly IUnitofWork _unitOfWork;

    public LookUpServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }
    public async Task<List<BookCategory>> BookCategories()
    {
        return await _unitOfWork.ILookUpRepository.BookCategories();
    }

    public async Task<List<FarqaCategory>> FarqaCategories()
    {
        return await _unitOfWork.ILookUpRepository.FarqaCategories();
    }

    public async Task<List<Scholar>> GetScholars()
    {
        return await _unitOfWork.ILookUpRepository.GetScholars();

    }


    public async Task<List<BookDetail>> GetBookDetails()
    {
        return await _unitOfWork.ILookUpRepository.GetBookDetails();
    }
    
    public async Task<List<BookImage>> GetBookImages()
    {
        return await _unitOfWork.ILookUpRepository.GetBookImages();
    }
    public async Task<List<MadrassaBook>> GetMadrassaBooks()
    {
        return await _unitOfWork.ILookUpRepository.GetMadrassaBooks();
    }


}

