using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.DTOs.Project;
using TaskManagementSystemApi.Models;
using TaskManagementSystemApi.Services.Interfaces;
using TaskManagementSystemApi.DTOs.Task;

namespace TaskManagementSystemApi.Services
{
    /// <summary>
    /// 專案服務實作
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 建構式注入 AppDbContext
        /// </summary>
        /// <param name="dbContext">資料庫內容物件</param>
        public ProjectService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 取得所有專案
        /// </summary>
        /// <returns>專案 DTO 清單</returns>
        public IEnumerable<ProjectDto> GetAllProjects()
        {
            var projects = _dbContext.Projects
                .Select(project => new ProjectDto
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectDescription = project.ProjectDescription,
                    CreatedByUserId = project.CreatedByUserId
                })
                .ToList();

            return projects;
        }

        /// <summary>
        /// 依照專案 ID 取得單一專案
        /// </summary>
        /// <param name="projectId">專案 ID</param>
        /// <returns>若存在則回傳 ProjectDto，否則回傳 null</returns>
        public ProjectDto? GetProjectById(int projectId)
        {
            var project = _dbContext.Projects
                .Where(project => project.ProjectId == projectId)
                .Select(project => new ProjectDto
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectDescription = project.ProjectDescription,
                    CreatedByUserId = project.CreatedByUserId
                })
                .SingleOrDefault();

            return project;
        }

        /// <summary>
        /// 建立新專案
        /// </summary>
        /// <param name="createProjectDto">建立專案 DTO</param>
        /// <returns>建立完成後的專案 DTO</returns>
        public ProjectDto CreateProject(CreateProjectDto createProjectDto)
        {
            // 建立新的 Project Entity
            var project = new Project
            {
                ProjectName = createProjectDto.ProjectName,
                ProjectDescription = createProjectDto.ProjectDescription,
                CreatedByUserId = createProjectDto.CreatedByUserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // 寫入資料庫
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            // 回傳建立完成後的 DTO
            var projectDto = new ProjectDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription,
                CreatedByUserId = project.CreatedByUserId
            };

            return projectDto;
        }

        /// <summary>
        /// 依照專案 ID 取得該專案底下的所有任務
        /// </summary>
        /// <param name="projectId">專案 ID</param>
        /// <returns>任務 DTO 清單</returns>
        public IEnumerable<TaskDto> GetTasksByProjectId(int projectId)
        {
            var tasks = _dbContext.Tasks
                .Where(task => task.ProjectId == projectId)
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
    }
}