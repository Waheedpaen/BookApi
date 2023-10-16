
using EntitiesClasses.Entities;
using ViewModels.AudioDetailViewModel;
using ViewModels.BookDetails;
using ViewModels.CommonViewModel;
using ViewModels.FarqaCategoryViewModel;
using ViewModels.MonthlyMagizne;
using ViewModels.ScholarViewModel;

namespace ViewModel.AutoMapper;
public class AutoMappers : Profile
    {
    public AutoMappers()
    {
        CreateMap<BookCategory, CommonDto>().ReverseMap();
        CreateMap<BookCategory, CommonDto>().ReverseMap();
        CreateMap<MonthlyMagzine, CommonDto>().ReverseMap();
        CreateMap<FarqaCategory, FarqaCategoryDto>().ReverseMap();
        CreateMap<FarqaCategory, FarqaCategorySaveDto>().ReverseMap();
        CreateMap<Scholar, ScholarDto>().ReverseMap();
        CreateMap<MonthlyMagzine, CommonDto>().ReverseMap();
        CreateMap<Scholar, ScholarSaveDto>().ReverseMap();
        CreateMap<MonthlyMagzine, MonthlyMagzinesSaveDto>().ReverseMap(); 
        CreateMap<BookDetail, BookDetailDto>().ReverseMap();
        CreateMap<AudioScholars, CommonDto>().ReverseMap();
        CreateMap< AudioDetail, CommonDto> ().ReverseMap();

        
        CreateMap<MadrassaClass, CommonDto>().ReverseMap();
        //CreateMap<BookDetail, BookDetailSaveDto>().ReverseMap(); 
        CreateMap<BookImage, BookImageDto>().ReverseMap();
        //CreateMap<BookImage, BookImageSaveDto>().ReverseMap(); 
        //CreateMap<User, UserListDto>()
        //       .ForMember(dest =>
        //       dest.Name,
        //       opt => opt.MapFrom(src => src.)).ReverseMap();

    }



}