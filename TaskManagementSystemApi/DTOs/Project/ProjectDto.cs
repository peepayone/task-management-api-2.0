namespace TaskManagementSystemApi.DTOs.Project
{
    /// <summary>
    /// 專案回傳 DTO
    /// </summary>
    public class ProjectDto
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public string? ProjectDescription { get; set; }

        public int CreatedByUserId { get; set; }
    }
}