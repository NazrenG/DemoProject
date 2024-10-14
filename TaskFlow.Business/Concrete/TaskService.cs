
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly ITaskDal dal;

        public TaskService(ITaskDal dal)
        {
            this.dal = dal;
        }

        public async  Task Add( TaskForUser task)
        { 
            await dal.Add(task);
        }

        public async Task Delete(TaskForUser task)
        {
           await dal.Delete(task);
        }

        public async Task<TaskForUser> GetTaskById(int id)
        {
            return await dal.GetById(f => f.Id == id);
        }

        public async Task<List<TaskForUser>> GetTasks()
        {
            return await dal.GetAll();
        }

        public async Task Update(TaskForUser task)
        {
          await dal.Update(task);
        }
    }
}
