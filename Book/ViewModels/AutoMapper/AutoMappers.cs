
using EntitiesClasses.Entities;
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
        //CreateMap<User, UserListDto>()
        //       .ForMember(dest =>
        //       dest.Name,
        //       opt => opt.MapFrom(src => src.)).ReverseMap();

    }



}