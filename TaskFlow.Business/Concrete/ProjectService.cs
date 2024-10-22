﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class ProjectService:IProjectService
    {
        private readonly IProjectDal dal;

        public ProjectService(IProjectDal dal)
        {
            this.dal = dal;
        }

        public async System.Threading.Tasks.Task Add(Project project)
        {
          await dal.Add(project);
        }

        public async System.Threading.Tasks.Task Delete(Project project)
        {
          await dal.Delete(project);
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await dal.GetById(p => p.Id == id);
        }

        public async Task<List<Project>> GetProjects()
        {
           return await dal.GetAll();
        }

        public async Task Update(Project project)
        {
           await dal.Update(project);
        }

        public async Task<int> GetUserProjectCount(string userId)
        {
            var list = await dal.GetAll(p => p.CreatedById == userId);
            return list.Count();    
        }
    }
}
