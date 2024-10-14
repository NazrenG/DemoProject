using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Abstract
{
    public interface ITeamService
    {  
        Task<List<Team>> GetTeams ();
        Task<Team> GetTeamById(int id);
       Task Add(Team team);
       Task Update(Team team);
       Task Delete(Team team);
    }
}
