

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
}
 
