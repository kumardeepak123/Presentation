using CPMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS.Repository
{
    public interface IProjectRepo
    {
        Task<bool> CreateProject(Project project, int[] TeamIds); //DONE
        Task<Project> GetProjectById(int id); //DONE
        Task<IEnumerable<Project>> GetAllProjects(); //DOME
        Task<bool> UpdateProject(int id, Project project, int[] TeamIds); //DONE
        Task<bool> DeleteProject(int id); //DONE
        Task<List<Project>> GetProjectsUnderClient(int id); //DONE
        Task<List<Project>> GetProjectsForAssignmentToClient(int id); //DONE Id refers to client id


       /* Task<Demo> ProjectName_WithClientName(int id);*/
    }  
}
