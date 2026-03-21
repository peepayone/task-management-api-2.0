using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.DTOs.Task;
using TaskManagementSystemApi.Models;
using TaskManagementSystemApi.Services.Interfaces;

namespace TaskManagementSystemApi.Services
{
    /// <summary>
    /// 任務服務實作
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 建構式注入 AppDbContext
        /// </summary>
        /// <param name="dbContext">資料庫內容物件</param>
        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 取得所有任務
        /// </summary>
        /// <returns>任務 DTO 清單</returns>
        public IEnumerable<TaskDto> GetAllTasks()
        {
            var tasks = _dbContext.Tasks
                .Select(task => new TaskDto
                {
                    TaskId = task.TaskId,
                    ProjectId = task.ProjectId,
                    TaskTitle = task.TaskTitle,
                    TaskDescription = task.TaskDescription,
                    TaskStatus = task.TaskStatus,
                    AssignedToUserId = task.AssignedToUserId,
                    CreatedByUserId = task.CreatedByUserId,
                    DueDate = task.DueDate
                })
                .ToList();

            return tasks;
        }

        /// <summary>
        /// 依照任務 ID 取得單一任務
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <returns>若存在則回傳 TaskDto，否則回傳 null</returns>
        public TaskDto? GetTaskById(int taskId)
        {
            var task = _dbContext.Tasks
                .Where(task => task.TaskId == taskId)
                .Select(task => new TaskDto
                {
                    TaskId = task.TaskId,
                    ProjectId = task.ProjectId,
                    TaskTitle = task.TaskTitle,
                    TaskDescription = task.TaskDescription,
                    TaskStatus = task.TaskStatus,
                    AssignedToUserId = task.AssignedToUserId,
                    CreatedByUserId = task.CreatedByUserId,
                    DueDate = task.DueDate
                })
                .SingleOrDefault();

            return task;
        }

        /// <summary>
        /// 建立新任務
        /// </summary>
        /// <param name="createTaskDto">建立任務 DTO</param>
        /// <returns>建立完成後的任務 DTO</returns>
        public TaskDto CreateTask(CreateTaskDto createTaskDto)
        {
            var task = new TaskItem
            {
                ProjectId = createTaskDto.ProjectId,
                TaskTitle = createTaskDto.TaskTitle,
                TaskDescription = createTaskDto.TaskDescription,
                TaskStatus = createTaskDto.TaskStatus,
                AssignedToUserId = createTaskDto.AssignedToUserId,
                CreatedByUserId = createTaskDto.CreatedByUserId,
                DueDate = createTaskDto.DueDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            var taskDto = new TaskDto
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskStatus = task.TaskStatus,
                AssignedToUserId = task.AssignedToUserId,
                CreatedByUserId = task.CreatedByUserId,
                DueDate = task.DueDate
            };

            return taskDto;
        }

        /// <summary>
        /// 更新任務
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <param name="updateTaskDto">更新任務 DTO</param>
        /// <returns>更新成功回傳 true，找不到任務回傳 false</returns>
        public bool UpdateTask(int taskId, UpdateTaskDto updateTaskDto)
        {
            var task = _dbContext.Tasks.SingleOrDefault(task => task.TaskId == taskId);

            if (task == null)
            {
                return false;
            }

            task.TaskTitle = updateTaskDto.TaskTitle;
            task.TaskDescription = updateTaskDto.TaskDescription;
            task.TaskStatus = updateTaskDto.TaskStatus;
            task.AssignedToUserId = updateTaskDto.AssignedToUserId;
            task.DueDate = updateTaskDto.DueDate;
            task.UpdatedAt = DateTime.Now;

            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// 刪除任務
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <returns>刪除成功回傳 true，找不到任務回傳 false</returns>
        public bool DeleteTask(int taskId)
        {
            var task = _dbContext.Tasks.SingleOrDefault(task => task.TaskId == taskId);

            if (task == null)
            {
                return false;
            }

            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();

            return true;
        }
    }
}