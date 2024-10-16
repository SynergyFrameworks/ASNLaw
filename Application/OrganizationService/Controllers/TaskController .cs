using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;
using ASNTask = Datalayer.Domain.Group.ASNTask;

namespace OrganizationService.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IService<ASNTask, DefaultSearch<TaskSearchResult>> _taskService;
        public TaskController(IService<ASNTask, DefaultSearch<TaskSearchResult>> taskService)
        {
            _taskService = taskService;
        }


        [HttpGet("{taskId}")]
        [Authorize(TaskPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid taskId)
        {
            return Ok(await _taskService.Get(TaskProjection.TaskInformation, taskId));
        }

        [HttpGet()]
        [Authorize(TaskPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _taskService.Get(TaskProjection.TaskInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(TaskPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<TaskSearchResult> search)
        {
            return Ok(await _taskService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(TaskPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(TaskInfo newTask)
        {
            if (!ModelState.IsValid) return BadRequest();

            ASNTask task = new ASNTask()
            {
                Name = newTask.Name,
                Description = newTask.Description,
                ShortName = newTask.ShortName,
                ProjectId = newTask.ProjectId,
                ModuleId = newTask.ModuleId,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _taskService.Add(task, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(TaskPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(TaskInfo updateTask)
        {
            if (!ModelState.IsValid && updateTask.Id == Guid.Empty) return BadRequest();

            ASNTask task = new ASNTask();
            task.Id = updateTask.Id;
            task.Name = updateTask.Name;
            task.Description = updateTask.Description;
            task.ShortName = updateTask.ShortName;
            task.ProjectId = updateTask.ProjectId;
            task.ModuleId = updateTask.ModuleId;
            task.ModifiedBy = "system";
            task.ModifiedDate = DateTime.UtcNow;

            return Ok(await _taskService.Update(task, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{taskId}")]
        [Authorize(TaskPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            if (taskId == Guid.Empty) return BadRequest();
            return base.Ok(await _taskService.Delete(new ASNTask { Id = taskId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
