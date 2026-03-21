namespace TaskManagementSystemApi.DTOs.Project
{
    /// <summary>
    /// 建立專案 DTO
    /// </summary>
    public class CreateProjectDto
    {
        public string ProjectName { get; set; } = string.Empty;

        public string? ProjectDescription { get; set; }

        public int CreatedByUserId { get; set; }
    }
}