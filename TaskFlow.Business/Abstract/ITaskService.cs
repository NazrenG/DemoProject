using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Models; 

namespace TaskFlow.DataAccess.Abstract
{
    public interface ITaskService
    {
        Task<List<TaskForUser>> GetTasks();
        Task<TaskForUser> GetTaskById(int id);
      Task Add(TaskForUser task);
      Task Update(TaskForUser task);
      Task Delete(TaskForUser task);

    }
}
