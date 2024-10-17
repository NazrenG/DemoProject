﻿using DemoProject.DTOs;
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
        [HttpPut("ChangeTitle/{id}")]
        public async Task<IActionResult> PutTitle(int id, [FromBody] string value)
        {
            var item = await taskService.GetTaskById(id);

            if (item == null)
            {
                return NotFound();
            } 
            //item.Description = value.Description;
            //item.Deadline = value.Deadline;
            //   item.Priority = value.Priority;
            //item.Status = value.Status;
            item.Title = value; 
            await taskService.Update(item);
            return Ok();
        }
        [HttpPut("ChangeDescription/{id}")]
        public async Task<IActionResult> PutDescription(int id, [FromBody] string value)
        {
            var item = await taskService.GetTaskById(id);

            if (item == null)
            {
                return NotFound();
            }
            item.Description = value; 
            await taskService.Update(item);
            return Ok();
        }

        [HttpPut("ChangeDeadLine/{id}")]
        public async Task<IActionResult> PutDeadLine(int id, [FromBody] DateTime value)
        {
            var item = await taskService.GetTaskById(id);

            if (item == null)
            {
                return NotFound();
            } 
            item.Deadline = value; 
            await taskService.Update(item);
            return Ok();
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<IActionResult> PutStatus(int id, [FromBody] string value)
        {
            var item = await taskService.GetTaskById(id);

            if (item == null)
            {
                return NotFound();
            } 
            item.Status = value; 
            await taskService.Update(item);
            return Ok();
        }
        [HttpPut("ChangePriority/{id}")]
        public async Task<IActionResult> PutPriority(int id, [FromBody] string value)
        {
            var item = await taskService.GetTaskById(id);

            if (item == null)
            {
                return NotFound();
            }
              item.Priority = value; 
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
