using Azure;
using HelperDatas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Services
{
 
    public  class BookCategoryServices : IBookCategoryServices
    {
        private readonly IUnitofWork _unitOfWork;

        public BookCategoryServices(IUnitofWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<BookCategory> BookCategoryAlreadyExit(string name)
        {
             return await _unitOfWork.IBookCategory.BookCategoryAlreadyExit(name);
        }

        public async Task<BookCategory> Create(BookCategory model)
        {
            model.Updated_At = null;
            await _unitOfWork.IBookCategory.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
        }

        public async Task<BookCategory> Delete(BookCategory model)
        {
            model.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return model;
        }

        public async Task<BookCategory> Get(int id)
        {
            return await _unitOfWork.IBookCategory.GetByIdAsync(id);
        }

        public Task<List<BookCategory>> GetBookCategory(SeachItem searchitem)
        {
            throw new NotImplementedException();
        }

        public async Task<BookCategory> Update(BookCategory update, BookCategory model)
        {
            update.Name = model.Name; 
            update.Updated_At = model.Updated_At;
            await _unitOfWork.CommitAsync();
            return update;
        }
    }



}
