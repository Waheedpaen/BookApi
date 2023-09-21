
using EntitiesClasses.Entities;
using ViewModels.BookDetails;
using ViewModels.CommonViewModel;
using ViewModels.FarqaCategoryViewModel;
using ViewModels.ScholarViewModel;

namespace ViewModel.AutoMapper;
public class AutoMappers : Profile
    {
    public AutoMappers()
    {
        CreateMap<BookCategory, CommonDto>().ReverseMap();
        CreateMap<BookCategory, CommonDto>().ReverseMap();
        CreateMap<FarqaCategory, FarqaCategoryDto>().ReverseMap();
        CreateMap<FarqaCategory, FarqaCategorySaveDto>().ReverseMap();
        CreateMap<Scholar, ScholarDto>().ReverseMap();
        CreateMap<Scholar, ScholarSaveDto>().ReverseMap();
        CreateMap<BookDetail, BookDetailDto>().ReverseMap();
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