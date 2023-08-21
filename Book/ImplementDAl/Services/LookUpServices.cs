

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
}
 
