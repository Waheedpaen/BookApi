using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories;

public interface IAudioScholarsRepository : IRepository<AudioScholars, int>
{
    public Task<AudioScholars> AudioScholarsAlreadyExit(string name); 
}
