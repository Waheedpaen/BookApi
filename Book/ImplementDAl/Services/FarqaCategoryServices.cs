

namespace ImplementDAl.Services;

public class FarqaCategoryServices : IFarqaCategoryServices
{
    private readonly IUnitofWork _unitOfWork;

    public FarqaCategoryServices(IUnitofWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<FarqaCategory> Create(FarqaCategory model)
    {
        model.Updated_At = null;
        await _unitOfWork.IFarqaCategoryRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<FarqaCategory> Delete(FarqaCategory model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;

    }

    public async Task<FarqaCategory> FarqaCategoryAlreadyExit(string name)
    {
        return  await _unitOfWork.IFarqaCategoryRepository.FarqaCategoryAlreadyExit(name) ;
    }

    public async Task<FarqaCategory> Get(int id)
    {
        return await _unitOfWork.IFarqaCategoryRepository.Get(id);
    }

    public async Task<List<FarqaCategory>> GetFarqaCategoriesByBook(int Id)
    {
        return await _unitOfWork.IFarqaCategoryRepository.GetFarqaCategoriesByBook(Id);
    }

    public async Task<FarqaCategory> Update(FarqaCategory update, FarqaCategory model)
    {
        update.Name = model.Name;
        update.BookCategoryId = model.BookCategoryId;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }
}
 
