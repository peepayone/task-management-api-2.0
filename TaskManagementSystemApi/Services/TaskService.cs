using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.DTOs.Task;
using TaskManagementSystemApi.Models;
using TaskManagementSystemApi.Services.Interfaces;
using TaskManagementSystemApi.Queries;

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
        public IEnumerable<TaskDto> GetAllTasks(TaskQueryParameters queryParameters)
        {
            // 先取得可延伸的 IQueryable，後續才能動態組查詢條件
            var query = _dbContext.Tasks.AsQueryable();

            // 篩選條件
            // 依照任務狀態篩選
            if (!string.IsNullOrWhiteSpace(queryParameters.TaskStatus))
            {
                query = query.Where(task => task.TaskStatus == queryParameters.TaskStatus);
            }

            // 依被指派者ID篩選
            if (queryParameters.AssignedToUserId.HasValue)
            {
                query = query.Where(task => task.AssignedToUserId == queryParameters.AssignedToUserId.Value);
            }

            // 依專案 ID 篩選
            if (queryParameters.ProjectId.HasValue)
            {
                query = query.Where(task => task.ProjectId == queryParameters.ProjectId.Value);
            }

            // 排序條件
            var sortBy = queryParameters.SortBy?.ToLower();
            var sortOrder = queryParameters.SortOrder?.ToLower();

            // 預設排序 TaskId asc
            switch (sortBy)
            {
                case "due_date":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(task => task.DueDate)
                        : query.OrderBy(task => task.DueDate);
                    break;
                case "created_at":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(task => task.CreatedAt)
                        : query.OrderBy(task => task.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(task => task.TaskId);
                    break;
            }

            // make DTO
            var tasks = query
                .Select(task => new TaskDto
                {
                    TaskId = task.TaskId,

                    ProjectId = task.ProjectId,
                    ProjectName = task.Project.ProjectName,

                    TaskTitle = task.TaskTitle,
                    TaskDescription = task.TaskDescription,
                    TaskStatus = task.TaskStatus,

                    AssignedToUserId = task.AssignedToUserId,
                    AssignedToUserName = task.AssignedToUser != null
                        ? task.AssignedToUser.UserName
                        : null,

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
                    ProjectName = task.Project.ProjectName,

                    TaskTitle = task.TaskTitle,
                    TaskDescription = task.TaskDescription,
                    TaskStatus = task.TaskStatus,

                    AssignedToUserId = task.AssignedToUserId,
                    AssignedToUserName = task.AssignedToUser != null
                        ? task.AssignedToUser.UserName
                        : null,

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