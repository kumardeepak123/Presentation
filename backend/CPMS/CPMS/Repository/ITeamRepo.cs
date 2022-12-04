using CPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Repository
{
    public interface ITeamRepo
    {
        Task<bool> CreateTeam(Team Team, int[] EmployeeIds);
        Task<Team> GetTeamById(int id);
        Task<bool> EditTeam(int id, Team team, int[] emloyeeIds);
        Task<bool> DeleteTeam(int id);
        Task<List<Team>> GetTeamsWithNoProject();
         Task<List<Team>> GetTeamsUnderProject(int id);
        Task<List<Team>> GetAllTeams();
    }
}
