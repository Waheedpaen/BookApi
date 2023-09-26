using EntitiesClasses.Entities;
using HelperDatas.GlobalReferences;
using HelperDatas.PaginationsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services;

public interface IMadrassaBookServices
{
    #region MadrassaBookCrud
    public Task<MadrassaBook> Get(int Id);
    public Task<MadrassaBook> MadrassaBookAlreadyExit(string name);
    Task<MadrassaBook> SaveMadrassaBook(MadrassaBook model); 
    Task<MadrassaBook> DeleteMadrassaBook(MadrassaBook model);
    Task<MadrassaBook> UpdateMadrassaBook(MadrassaBook update, MadrassaBook model); 
    Task<PagedResult<MadrassaBook>> SearchAndPaginateAsync(SearchAndPaginateOptions options);
    #endregion

    #region MadrassaBookCatogryCrud
    Task<MadrassaBookCatgory> DeleteMadrassaBookCatgoryImages(MadrassaBookCatgory model);
    Task<MadrassaBookCatgory> GetMadrassaBookCatgoryById(int Id);
    Task<MadrassaBookCatgory> SaveMadrassaBookCatgoryImages(MadrassaBookCatgory model);
    Task<MadrassaBookCatgory> CreateMadrassaBookCatgoryImages(MadrassaBookCatgory model);
    Task<MadrassaBookCatgory> UpdateMadrassaBookCatgory(MadrassaBookCatgory update, MadrassaBookCatgory model);

    Task<MadrassaBookCatgory> DeleteMadrassaBookCatgoryImagePermently(MadrassaBookCatgory model);
    Task<List<MadrassaBookCatgory>> MadrassaBookCatgorybyMadrassBook(int Id);

    #endregion

}
