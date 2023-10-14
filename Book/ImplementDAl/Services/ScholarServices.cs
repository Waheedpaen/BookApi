 
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System.Linq.Expressions;

namespace ImplementDAl.Services;
 
    public class ScholarServices : IScholarServices
    {
        private readonly IUnitofWork _unitOfWork;

        public ScholarServices(IUnitofWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public async Task<Scholar> Create(Scholar model)
        {
            model.Updated_At = null;
            await _unitOfWork.IScholarRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
        }

        public async Task<Scholar> Delete(Scholar model)
        {
            model.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return model;
        }

        public async Task<Scholar> ScholarNameAlreadyExit(string name)
        {
            return await _unitOfWork.IScholarRepository.ScholarNameAlreadyExit(name);
        }

        public async Task<Scholar> Get(int id)
        {
            return await _unitOfWork.IScholarRepository.Get(id);
        }

        public async Task<List<Scholar>> GetScholarByFarqaCategories(int Id)
        {
            return await _unitOfWork.IScholarRepository.GetScholarByFarqaCategories(Id);
        }

        public async Task<Scholar> Update(Scholar update, Scholar model)
        {
            update.Name = model.Name;
            update.ImageUrl = model.ImageUrl;
            update.MadrassaName = model.MadrassaName;
            update.FarqaCategoryId = model.FarqaCategoryId;
            update.Updated_At = model.Updated_At;
            await _unitOfWork.CommitAsync();
            return update;
        }


    public async Task<PagedResult<Scholar>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
    {
        Expression<Func<Scholar, bool>> predicate = category =>
        string.IsNullOrEmpty(options.SearchTerm) ||
        category.Name.Contains(options.SearchTerm);

        var pagedResult = await _unitOfWork.IScholarRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
        return pagedResult;
    }

}
 
