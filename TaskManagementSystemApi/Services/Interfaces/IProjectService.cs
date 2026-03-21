using TaskManagementSystemApi.DTOs.Project;
using TaskManagementSystemApi.DTOs.Task;

namespace TaskManagementSystemApi.Services.Interfaces
{
    /// <summary>
    /// 專案服務介面
    /// 定義專案相關的商業邏輯。
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// 取得所有專案
        /// </summary>
        /// <returns>專案 DTO 清單</returns>
        IEnumerable<ProjectDto> GetAllProjects();

        /// <summary>
        /// 依照專案 ID 取得單一專案
        /// </summary>
        /// <param name="projectId">專案 ID</param>
        /// <returns>若存在則回傳 ProjectDto，否則回傳 null</returns>
        ProjectDto? GetProjectById(int projectId);

        /// <summary>
        /// 建立新專案
        /// </summary>
        /// <param name="createProjectDto">建立專案 DTO</param>
        /// <returns>建立完成後的專案 DTO</returns>
        ProjectDto CreateProject(CreateProjectDto createProjectDto);

        /// <summary>
        /// 依照專案 ID 取得該專案底下的所有任務
        /// </summary>
        /// <param name="projectId">專案 ID</param>
        /// <returns>任務 DTO 清單</returns>
        IEnumerable<TaskDto> GetTasksByProjectId(int projectId);
    }
}