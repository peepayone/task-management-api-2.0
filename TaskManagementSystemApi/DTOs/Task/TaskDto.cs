namespace TaskManagementSystemApi.DTOs.Task
{
    /// <summary>
    /// 任務回傳 DTO
    /// </summary>
    public class TaskDto
    {
        /// <summary>
        /// 任務 ID
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 所屬專案 ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 任務標題
        /// </summary>
        public string TaskTitle { get; set; } = string.Empty;

        /// <summary>
        /// 任務描述
        /// </summary>
        public string? TaskDescription { get; set; }

        /// <summary>
        /// 任務狀態
        /// </summary>
        public string TaskStatus { get; set; } = string.Empty;

        /// <summary>
        /// 被指派者使用者 ID
        /// </summary>
        public int? AssignedToUserId { get; set; }

        /// <summary>
        /// 建立者使用者 ID
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}