using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemApi.DTOs.Task;
using TaskManagementSystemApi.Services.Interfaces;
using TaskManagementSystemApi.Queries;

namespace TaskManagementSystemApi.Controllers
{
    /// <summary>
    /// 任務 API 控制器
    /// </summary>
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// 建構式注入任務服務
        /// </summary>
        /// <param name="taskService">任務服務</param>
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// 取得所有任務，並支援篩選與排序
        /// GET: /api/tasks
        /// </summary>
        /// <param name="queryParameters">任務查詢參數</param>
        /// <returns>任務清單</returns>
        [HttpGet]
        public IActionResult GetAllTasks([FromQuery] TaskQueryParameters queryParameters)
        {
            var tasks = _taskService.GetAllTasks(queryParameters);

            return Ok(tasks);
        }

        /// <summary>
        /// 依照任務 ID 取得單一任務
        /// GET: /api/tasks/{id}
        /// </summary>
        /// <param name="id">任務 ID</param>
        /// <returns>單一任務資料</returns>
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        /// <summary>
        /// 建立新任務
        /// POST: /api/tasks
        /// </summary>
        /// <param name="createTaskDto">建立任務 DTO</param>
        /// <returns>建立完成後的任務資料</returns>
        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTask = _taskService.CreateTask(createTaskDto);

            return CreatedAtAction(
                nameof(GetTaskById),
                new { id = createdTask.TaskId },
                createdTask
            );
        }

        /// <summary>
        /// 更新任務
        /// PUT: /api/tasks/{id}
        /// </summary>
        /// <param name="id">任務 ID</param>
        /// <param name="updateTaskDto">更新任務 DTO</param>
        /// <returns>更新成功回傳 204，找不到任務回傳 404</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = _taskService.UpdateTask(id, updateTaskDto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// 刪除任務
        /// DELETE: /api/tasks/{id}
        /// </summary>
        /// <param name="id">任務 ID</param>
        /// <returns>刪除成功回傳 204，找不到任務回傳 404</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var deleted = _taskService.DeleteTask(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}