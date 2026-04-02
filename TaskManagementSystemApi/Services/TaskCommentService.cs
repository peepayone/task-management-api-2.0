using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.DTOs.TaskComment;
using TaskManagementSystemApi.Models;
using TaskManagementSystemApi.Services.Interfaces;

namespace TaskManagementSystemApi.Services
{
    /// <summary>
    /// 任務留言服務實作
    /// </summary>
    public class TaskCommentService : ITaskCommentService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 建構式注入 AppDbContext
        /// </summary>
        /// <param name="dbContext">資料庫內容物件</param>
        public TaskCommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 依照任務 ID 取得該任務的所有留言
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <returns>任務留言 DTO 清單</returns>
        public IEnumerable<TaskCommentDto> GetCommentsByTaskId(int taskId)
        {
            var taskComments = _dbContext.TaskComments
                .Where(taskComment => taskComment.TaskId == taskId)
                .Select(taskComment => new TaskCommentDto
                {
                    TaskCommentId = taskComment.TaskCommentId,
                    TaskId = taskComment.TaskId,
                    UserId = taskComment.UserId,
                    UserName = taskComment.User.UserName,
                    TaskCommentContent = taskComment.TaskCommentContent
                })
                .ToList();

            return taskComments;
        }

        /// <summary>
        /// 建立新的任務留言
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <param name="createTaskCommentDto">建立留言 DTO</param>
        /// <returns>建立完成後的留言 DTO；若 task 不存在則回傳 null</returns>
        public TaskCommentDto? CreateTaskComment(int taskId, CreateTaskCommentDto createTaskCommentDto)
        {
            // 先確認任務是否存在
            var taskExists = _dbContext.Tasks.Any(task => task.TaskId == taskId);

            if (!taskExists)
            {
                return null;
            }

            var taskComment = new TaskComment
            {
                TaskId = taskId,
                UserId = createTaskCommentDto.UserId,
                TaskCommentContent = createTaskCommentDto.TaskCommentContent,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _dbContext.TaskComments.Add(taskComment);
            _dbContext.SaveChanges();

            var createdComment = _dbContext.TaskComments
                .Include(tc => tc.User)
                .First(tc => tc.TaskCommentId == taskComment.TaskCommentId);

            var taskCommentDto = new TaskCommentDto
            {
                TaskCommentId = createdComment.TaskCommentId,
                TaskId = createdComment.TaskId,
                UserId = createdComment.UserId,
                UserName = createdComment.User.UserName,
                TaskCommentContent = createdComment.TaskCommentContent
            };

            return taskCommentDto;
        }

        /// <summary>
        /// 更新任務留言
        /// </summary>
        /// <param name="taskCommentId">留言 ID</param>
        /// <param name="updateTaskCommentDto">更新留言 DTO</param>
        /// <returns>更新成功回傳 true，找不到留言回傳 false</returns>
        public bool UpdateTaskComment(int taskCommentId, UpdateTaskCommentDto updateTaskCommentDto)
        {
            var taskComment = _dbContext.TaskComments
                .SingleOrDefault(taskComment => taskComment.TaskCommentId == taskCommentId);

            if (taskComment == null)
            {
                return false;
            }

            taskComment.TaskCommentContent = updateTaskCommentDto.TaskCommentContent;
            taskComment.UpdatedAt = DateTime.Now;

            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// 刪除任務留言
        /// </summary>
        /// <param name="taskCommentId">留言 ID</param>
        /// <returns>刪除成功回傳 true，找不到留言回傳 false</returns>
        public bool DeleteTaskComment(int taskCommentId)
        {
            var taskComment = _dbContext.TaskComments
                .SingleOrDefault(taskComment => taskComment.TaskCommentId == taskCommentId);

            if (taskComment == null)
            {
                return false;
            }

            _dbContext.TaskComments.Remove(taskComment);
            _dbContext.SaveChanges();

            return true;
        }
    }
}