﻿using CPMS.DBConnect;
using CPMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Repository
{
    public class TeamRepo : ITeamRepo
    {
        public readonly CPMDbContext _CPMDbContext;

        public TeamRepo(CPMDbContext cPMDbContext)
        {
            _CPMDbContext = cPMDbContext;
        }

        public async Task<bool> CreateTeam(Team Team, int[] EmployeeIds)
        {
            var _Team = new Team
            {
                Name = Team.Name
            };

            try
            {
                _CPMDbContext.Teams.Add(_Team);
                await _CPMDbContext.SaveChangesAsync();

                foreach (var eid in EmployeeIds)
                {
                    var _Employee = await _CPMDbContext.Employees.Where(x => x.Id == eid).FirstOrDefaultAsync();
                    _Employee.TeamId = _Team.Id;
                    await _CPMDbContext.SaveChangesAsync();

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;

        }

        public async Task<bool> DeleteTeam(int id)
        {
            var _Team = await _CPMDbContext.Teams.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (_Team == null) return false;

            _CPMDbContext.Teams.Remove(_Team);
            var _Employees = await _CPMDbContext.Employees.Where(e => e.TeamId == id).ToListAsync();
            if(_Employees!=null && _Employees.Count >= 1)
            {
                foreach(var e in _Employees)
                {
                    e.TeamId = null;
                }
            }
            await _CPMDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditTeam(int id, Team team, int[] employeeIds)
        {
            var _Team =  await _CPMDbContext.Teams.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (_Team == null) return false;

            _Team.Name = team.Name;
            var _Employees = await _CPMDbContext.Employees.Where(x => x.TeamId == id).ToListAsync();
            foreach (var e in _Employees)
            {
                
                e.TeamId = null;
            }

            foreach(var i in employeeIds)
            {
                var _Employee = await _CPMDbContext.Employees.Where(x => x.Id == i).FirstOrDefaultAsync();
                _Employee.TeamId = id;
            }

            try
            {
                await _CPMDbContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            var _Teams = await _CPMDbContext.Teams.Select(x => new Team
            {
                Id = x.Id,
                Name = x.Name,
                Project = x.Project,
                ProjectId= x.ProjectId,
                Employees = x.Employees
            }).ToListAsync();
            return _Teams;
        }

        public async Task<Team> GetTeamById(int id)
        {
            var _Team = await  _CPMDbContext.Teams.Select(x => new Team
            {   
                Id = x.Id,
                Name = x.Name,
                Project = x.Project,
                Employees = x.Employees
            }).Where(p => p.Id == id).FirstOrDefaultAsync();

            return _Team;
        }

        public async Task<List<Team>> GetTeamsUnderProject(int id)
        {
            return await _CPMDbContext.Teams.Where(t => t.ProjectId == id).ToListAsync();
        }

        public async Task<List<Team>> GetTeamsWithNoProject()
        {
            var _Teams = await  _CPMDbContext.Teams.Where(t => t.ProjectId == null).Select(x=>new Team { 
                Id= x.Id,
                Name= x.Name,
                Employees = x.Employees
            }).ToListAsync();
            return _Teams;
        }

    }
}
