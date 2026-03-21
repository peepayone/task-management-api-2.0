namespace TaskManagementSystemApi.DTOs.Project
{
    /// <summary>
    /// 更新專案 DTO
    /// </summary>
    public class UpdateProjectDto
    {
        public string ProjectName { get; set; } = string.Empty;

        public string? ProjectDescription { get; set; }
    }
}