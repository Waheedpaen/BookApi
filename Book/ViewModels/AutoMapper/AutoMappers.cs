
using EntitiesClasses.Entities;
using ViewModels.CommonViewModel;

namespace ViewModel.AutoMapper;
public class AutoMappers : Profile
    {
    public AutoMappers()
    {
        CreateMap<BookCategory, CommonDto>().ReverseMap();
        //CreateMap<User, UserListDto>()
        //       .ForMember(dest =>
        //       dest.Name,
        //       opt => opt.MapFrom(src => src.)).ReverseMap();

    }



}