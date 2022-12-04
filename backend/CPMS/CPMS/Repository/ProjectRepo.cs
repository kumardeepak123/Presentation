using CPMS.DBConnect;
using CPMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Repository
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly CPMDbContext cPMDbContext;

        public ProjectRepo(CPMDbContext cPMDbContext)
        {
            this.cPMDbContext = cPMDbContext;
        }

        
        public async Task<bool> CreateProject(Project project, int[] TeamIds)
        {

            var _Project = new Project
            {
                Name = project.Name,
                FRequirement = project.FRequirement,
                NFRequirement = project.NFRequirement,
                Budget = project.Budget,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Technology = project.Technology
            };
            cPMDbContext.Projects.Add(_Project) ;

            try
            {
                await cPMDbContext.SaveChangesAsync();

                foreach(var tid in TeamIds)
                {
                    var _Team =  await cPMDbContext.Teams.Where(x => x.Id == tid).FirstOrDefaultAsync();
                    _Team.ProjectId = _Project.Id;
                    await cPMDbContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }

       
        public async Task<bool> DeleteProject(int id)
        {
            var project = await cPMDbContext.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (project == null) return false;

            var _Teams = await cPMDbContext.Teams.Where(x => x.ProjectId == id).ToListAsync();
            if(_Teams != null && _Teams.Count >= 1)
            {
                foreach(var t in _Teams)
                {
                    t.ProjectId = null;

                }
            }

            var _client_projects = await cPMDbContext.Client_Projects.Where(x => x.ProjectId == id).ToListAsync();
            if(_client_projects!= null && _client_projects.Count >= 1)
            {
                foreach(var r in _client_projects)
                {
                    cPMDbContext.Client_Projects.Remove(r);
                }
            }
            cPMDbContext.Projects.Remove(project);

            await cPMDbContext.SaveChangesAsync();
            return true;
        }

       
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            var projects = await cPMDbContext.Projects.ToListAsync();
            return projects;
        }

       
        public async Task<Project> GetProjectById(int id)
        {
            var project = await cPMDbContext.Projects.Where(p => p.Id == id).Select(x => new Project
            {   Id= x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Budget = x.Budget,
                FRequirement = x.FRequirement,
                NFRequirement = x.NFRequirement,
                Technology = x.Technology,
                Teams = x.Teams.Select(t => new Team {
                    Id= t.Id,
                    Name = t.Name,
                    Employees= t.Employees
                }).ToList(),

            }).FirstOrDefaultAsync();
            
            return project;
        }

        
        public async Task<List<Project>> GetProjectsUnderClient(int id)
        {
            /*var _Client_projects = await cPMDbContext.Client_Projects.Where(x => x.ClientId == id).ToListAsync();
            List<Project> _Projects = new List<Project>();
            foreach(var r in _Client_projects)
            {
                var _Project = await cPMDbContext.Projects.Where(x => x.Id == r.ProjectId).FirstOrDefaultAsync();
                _Projects.Add(_Project);
            }

            return _Projects;*/
            var _Client = await cPMDbContext.Clients.Where(x => x.Id == id).Select(x=> new Client { 
                Id = x.Id,
                Client_Projects= x.Client_Projects
            }).FirstOrDefaultAsync();
            List<Project> _Projects = new List<Project>();
            foreach(var e in _Client.Client_Projects)
            {
                var _Project = await cPMDbContext.Projects.Where(x => x.Id == e.ProjectId).Select(x=> new Project { 
                    Id = x.Id,
                    Name= x.Name,
                    FRequirement = x.FRequirement,
                    NFRequirement= x.NFRequirement,
                    Budget =x.Budget,
                    StartDate =x.StartDate,
                    EndDate =x.EndDate,
                    Technology =x.Technology
                }).FirstOrDefaultAsync();
                _Projects.Add(_Project);
            }

            return _Projects;
        }

        
        public async Task<List<Project>> GetProjectsForAssignmentToClient(int id)
        {
            var projects = await cPMDbContext.Projects.Select(x=> new Project { 
                Id= x.Id,
                Name= x.Name,
                FRequirement=x.FRequirement,
                NFRequirement= x.NFRequirement,
                Budget= x.Budget,
                Technology =x.Technology
            }).ToListAsync();

            HashSet<int> _ProjectIds = new HashSet<int>();
            var _ClientProjects = await cPMDbContext.Client_Projects.ToListAsync();

            foreach(var e in _ClientProjects)
            {
                if(e.ClientId == id)
                {
                    _ProjectIds.Add((int)e.ProjectId);
                }
            }
            if(_ProjectIds.Count == 0)
            {
                return projects;
            }


            return await cPMDbContext.Projects.Where(x => _ProjectIds.Contains(x.Id) == false).Select(x => new Project
            {
                Id = x.Id,
                Name = x.Name,
                FRequirement = x.FRequirement,
                NFRequirement = x.NFRequirement,
                Budget = x.Budget,
                Technology = x.Technology
            }).ToListAsync();
        }

        
        public async Task<bool> UpdateProject(int id, Project project, int[] TeamIds)
        {
            var proj = await cPMDbContext.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (proj == null) return false;

            proj.StartDate = project.StartDate;
            proj.EndDate = project.EndDate;
            proj.FRequirement = project.FRequirement;
            proj.NFRequirement = project.NFRequirement;
            proj.Name = project.Name;
            proj.Technology = project.Technology;
            proj.Budget = project.Budget;

            cPMDbContext.Projects.Update(proj);
            var _Teams = await  cPMDbContext.Teams.Where(t => t.ProjectId == id).ToListAsync();
            foreach(var t in _Teams)
            {
                t.ProjectId = null;
            }
            foreach(var i in TeamIds)
            {
                var team = await cPMDbContext.Teams.Where(t => t.Id == i).FirstOrDefaultAsync();
                team.ProjectId = id;
            }
            try
            {
                await cPMDbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

       /* public async Task<Demo> ProjectName_WithClientName(int id)
        {

            var res = await cPMDbContext.Projects.Where(p => p.Id == id).Select(p =>  new Demo
            {
                ProjectName = p.Name,
                Client = p._Client

            }).FirstOrDefaultAsync();

            return res;
        }*/

      
    }

    
}
