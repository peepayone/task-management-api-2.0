using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemApi.DTOs.TaskComment;
using TaskManagementSystemApi.Services.Interfaces;

namespace TaskManagementSystemApi.Controllers
{
    /// <summary>
    /// 任務留言 API 控制器
    /// </summary>
    [ApiController]
    public class TaskCommentsController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;

        /// <summary>
        /// 建構式注入任務留言服務
        /// </summary>
        /// <param name="taskCommentService">任務留言服務</param>
        public TaskCommentsController(ITaskCommentService taskCommentService)
        {
            _taskCommentService = taskCommentService;
        }

        /// <summary>
        /// 依照任務 ID 取得該任務的所有留言
        /// GET: /api/tasks/{id}/comments
        /// </summary>
        /// <param name="id">任務 ID</param>
        /// <returns>任務留言清單</returns>
        [HttpGet("api/tasks/{id}/comments")]
        public IActionResult GetCommentsByTaskId(int id)
        {
            var taskComments = _taskCommentService.GetCommentsByTaskId(id);

            return Ok(taskComments);
        }

        /// <summary>
        /// 建立新的任務留言
        /// POST: /api/tasks/{id}/comments
        /// </summary>
        /// <param name="id">任務 ID</param>
        /// <param name="createTaskCommentDto">建立留言 DTO</param>
        /// <returns>建立完成後的留言資料；若 task 不存在則回傳 404</returns>
        [HttpPost("api/tasks/{id}/comments")]
        public IActionResult CreateTaskComment(int id, [FromBody] CreateTaskCommentDto createTaskCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTaskComment = _taskCommentService.CreateTaskComment(id, createTaskCommentDto);

            if (createdTaskComment == null)
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(GetCommentsByTaskId),
                new { id },
                createdTaskComment
            );
        }

        /// <summary>
        /// 更新任務留言
        /// PUT: /api/task-comments/{id}
        /// </summary>
        /// <param name="id">留言 ID</param>
        /// <param name="updateTaskCommentDto">更新留言 DTO</param>
        /// <returns>更新成功回傳 204，找不到留言回傳 404</returns>
        [HttpPut("api/task-comments/{id}")]
        public IActionResult UpdateTaskComment(int id, [FromBody] UpdateTaskCommentDto updateTaskCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = _taskCommentService.UpdateTaskComment(id, updateTaskCommentDto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// 刪除任務留言
        /// DELETE: /api/task-comments/{id}
        /// </summary>
        /// <param name="id">留言 ID</param>
        /// <returns>刪除成功回傳 204，找不到留言回傳 404</returns>
        [HttpDelete("api/task-comments/{id}")]
        public IActionResult DeleteTaskComment(int id)
        {
            var deleted = _taskCommentService.DeleteTaskComment(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}