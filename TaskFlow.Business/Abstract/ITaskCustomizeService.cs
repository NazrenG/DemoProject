using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Abstract
{
    public interface ITaskCustomizeService
    {  
        Task<List<TaskCustomize>> GetTeams ();
        Task<TaskCustomize> GetTeamById(int id);
       Task Add(TaskCustomize taskCustomize);
       Task Update(TaskCustomize taskCustomize);
       Task Delete(TaskCustomize taskCustomize);
    }
}
