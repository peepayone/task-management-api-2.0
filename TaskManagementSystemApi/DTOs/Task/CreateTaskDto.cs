using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystemApi.DTOs.Task
{
    /// <summary>
    /// 建立任務 DTO
    /// </summary>
    public class CreateTaskDto
    {
        /// <summary>
        /// 所屬專案 ID
        /// </summary>
        [Required]
        public int ProjectId { get; set; }

        /// <summary>
        /// 任務標題
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string TaskTitle { get; set; } = string.Empty;

        /// <summary>
        /// 任務描述
        /// </summary>
        [MaxLength(2000)]
        public string? TaskDescription { get; set; }

        /// <summary>
        /// 任務狀態
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string TaskStatus { get; set; } = string.Empty;

        /// <summary>
        /// 被指派者使用者 ID
        /// </summary>
        public int? AssignedToUserId { get; set; }

        /// <summary>
        /// 建立者使用者 ID
        /// 實務上可由目前使用者帶入
        /// </summary>
        [Required]
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}