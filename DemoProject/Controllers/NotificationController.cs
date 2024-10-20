using DemoProject.DTOs;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Business.Abstract;
using TaskFlow.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        // GET: api/<NotificationController>
        [HttpGet("Notifications")]
        public async Task<IActionResult> Get()
        {
            var list = await notificationService.GetNotifications();
            var items = list.Select(p =>
            {
                return new NotificationDto
                {
                    Text = p.Text,
                    UserId = p.UserId,
                };
            });
            return Ok(items);
        }
        [HttpGet("NotificationCount")]
        public async Task<IActionResult> GetCount()
        {
            var count = await notificationService.GetCount();
          
            return Ok(count);
        }



        // POST api/<NotificationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotificationDto value)
        {

            var item = new Notification
            {
                Text = value.Text,
                UserId = value.UserId,
            };
            await notificationService.Add(item);
            return Ok(item);
        }


        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await notificationService.GetNotificationById(id);
            if (item == null) return NotFound();
            await notificationService.Delete(item);
            return Ok();
        }
    }
}
