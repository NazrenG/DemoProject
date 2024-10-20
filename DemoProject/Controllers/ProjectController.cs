using DemoProject.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/<ProjectController>
        [HttpGet("AllProjects")]
        public async Task<IActionResult> Get()
        {
            var list = await _projectService.GetProjects();
            var items = list.Select(p =>
            {
                return new ProjectDto
                {
                    CreatedById = p.CreatedById, 
                    Description = p.Description,
                    IsCompleted = p.IsCompleted,
                    Title = p.Title,
                };
            });
            return Ok(items);
        }

        [HttpGet("UserProjectCount")]
        public async Task<IActionResult> GetUserProjectCount(int id)
        {
            var count = await _projectService.GetUserProjectCount(id);
            
            return Ok(count);
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item=await _projectService.GetProjectById(id);
            if (item == null)
            {
                return NotFound();
            }
            var project = new ProjectDto
            {
                CreatedById = item.CreatedById,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                Title = item.Title,
            };
            return Ok(project);

        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDto value)
        {
             
            var item = new Project
            {
                CreatedById = value.CreatedById,
                Description = value.Description,
                IsCompleted = value.IsCompleted,
                Title = value.Title,
            };
            await _projectService.Add(item);
            return Ok(item);   
        }

        // PUT api/<ProjectController>/5
   
        [HttpPut("ChangeTitle/{id}")]
        public async Task<IActionResult> PutTitle(int id, [FromBody] string value)
        {
            var item = await _projectService.GetProjectById(id);

            if (item == null)
            {
                return NotFound();
            }
            item.Title = value;
            await _projectService.Update(item);
            return Ok();
        }

        [HttpPut("ChangeDescription/{id}")]
        public async Task<IActionResult> PutDescription(int id, [FromBody] string value)
        {
            var item = await _projectService.GetProjectById(id);

            if (item == null)
            {
                return NotFound();
            }
            item.Description = value;
            await _projectService.Update(item);
            return Ok();
        }

        [HttpPut("ChangeCompleted/{id}")]
        public async Task<IActionResult> PutCompleted(int id, [FromBody] bool value)
        {
            var item = await _projectService.GetProjectById(id);

            if (item == null)
            {
                return NotFound();
            }
            item.IsCompleted = value;
            await _projectService.Update(item);
            return Ok();
        }
        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item=await _projectService.GetProjectById(id);
            if (item == null)
            {
                return NotFound();
            }
            await _projectService.Delete(item); 
            return Ok(item);
        }
    }
}
