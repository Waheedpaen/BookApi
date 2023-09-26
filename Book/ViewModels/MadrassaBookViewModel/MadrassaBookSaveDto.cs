 

namespace ViewModels.MadrassaBookViewModel;

public class MadrassaBookSaveDto 
{

    public MadrassaBookSaveDto()
    {
        this.MadrassaBookCatgories = new List<MadrassaBookCatgorySaveDto>();
    }
    public int? Id { get; set; }
    public string Name { get; set; }
    public int ? MadrassaClassId { get; set; }
    public string  ? ImageUrl { get; set; }
    public virtual List<MadrassaBookCatgorySaveDto> MadrassaBookCatgories { get; set; }
}
