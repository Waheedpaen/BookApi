using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities
{
    public  class MadrassaBook : CommonClass
    {
        public MadrassaBook()
        {
            this.MadrassaBookCatgories = new List<MadrassaBookCatgory>();
        }
        [ForeignKey("MadrassaClass")]
        public int   MadrassaClassId { get; set; }
        public virtual MadrassaClass  MadrassaClass { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<MadrassaBookCatgory>  MadrassaBookCatgories { get; set; }
    }
}
