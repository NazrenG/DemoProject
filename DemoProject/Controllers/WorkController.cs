using DemoProject.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.DataAccess.Concrete;
using TaskFlow.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly ITaskService taskService;

        public WorkController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        // GET: api/<WorkController>
        [HttpGet("AllWorks")]
        public async Task<IEnumerable<WorkDto>> Get()
        {
            var list = await taskService.GetTasks();
            var items = list.Select(p =>
            {
                return new WorkDto
                {
                    CreatedById = p.CreatedById,
                    Description = p.Description,
                    Deadline = p.Deadline,
                    Priority = p.Priority,
                    Status = p.Status,
                    Title = p.Title,
                    ProjectId = p.ProjectId,
                };
            });
            return items;
        }


        // GET api/<WorkController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await taskService.GetTaskById(id);
            if (item == null)
            {
                return NotFound();
            }
            var work = new WorkDto
            {
                CreatedById = item.CreatedById,
                Description = item.Description,
                Deadline = item.Deadline,
                Priority = item.Priority,
                Status = item.Status,
                Title = item.Title,
                ProjectId = item.ProjectId,
            };
            return Ok(work);
        }

        // POST api/<WorkController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkDto value)
        {

            var item = new Work
            {
                CreatedById = value.CreatedById,
                Description = value.Description,
                Deadline = value.Deadline,
                Priority = value.Priority,
                Status = value.Status,
                Title = value.Title,
                ProjectId = value.ProjectId,
            };
            await taskService.Add(item);
            return Ok(item);
        }

        // PUT api/<WorkController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] WorkDto value)
        {
            var item = await taskService.GetTaskById(id);

            if (item == null)
            {
                return NotFound();
            }
            item.CreatedById = value.CreatedById;
            item.Description = value.Description;
            item.Deadline = value.Deadline;
               item.Priority = value.Priority;
            item.Status = value.Status;
            item.Title = value.Title;
            item.ProjectId = value.ProjectId;
            await taskService.Update(item);
            return Ok();
        }

        // DELETE api/<WorkController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await taskService.GetTaskById(id);
            if (item == null)
            {
                return NotFound();
            }
            await taskService.Delete(item);
            return Ok(item);
        }
    }
}
