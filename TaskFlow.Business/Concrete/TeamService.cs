using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class TeamService : ITeamService
    {
        private readonly ITeamDal dal;

        public TeamService(ITeamDal dal)
        {
            this.dal = dal;
        }

        public async Task Add(Team team)
        {
           await dal.Add(team);    
        }

        public async Task Delete(Team team)
        {
            await dal
                  .Delete(team);
        }

        public async Task<Team> GetTeamById(int id)
        {
           return await dal.GetById(f=> f.Id == id);    
        }

        public async Task<List<Team>> GetTeams()
        {
            return await dal.GetAll();
        }

        public async Task Update(Team team)
        {
          await dal .Update(team);
        }
    }
}
