namespace TaskManagementSystemApi.Models
{
    /// <summary>
    /// 任務 Entity
    /// 對應資料表：task
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// 任務主鍵 ID
        /// 對應欄位：task_id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 所屬專案 ID
        /// 對應欄位：project_id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 任務標題
        /// 對應欄位：task_title
        /// </summary>
        public string TaskTitle { get; set; } = string.Empty;

        /// <summary>
        /// 任務描述
        /// 對應欄位：task_description
        /// </summary>
        public string? TaskDescription { get; set; }

        /// <summary>
        /// 任務狀態
        /// 對應欄位：task_status
        /// 建議值：todo / doing / done
        /// </summary>
        public string TaskStatus { get; set; } = string.Empty;

        /// <summary>
        /// 被指派者使用者 ID
        /// 對應欄位：assigned_to_user_id
        /// 可為空，表示尚未指派
        /// </summary>
        public int? AssignedToUserId { get; set; }

        /// <summary>
        /// 建立者使用者 ID
        /// 對應欄位：created_by_user_id
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// 任務截止日期
        /// 對應欄位：due_date
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// 建立時間
        /// 對應欄位：created_at
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最後更新時間
        /// 對應欄位：updated_at
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        // =========================
        // Navigation Properties
        // =========================

        /// <summary>
        /// 此任務所屬的專案
        /// </summary>
        public Project Project { get; set; } = null!;

        /// <summary>
        /// 此任務的建立者
        /// </summary>
        public User CreatedByUser { get; set; } = null!;

        /// <summary>
        /// 此任務的被指派者
        /// 可為空
        /// </summary>
        public User? AssignedToUser { get; set; }

        /// <summary>
        /// 此任務底下的留言集合
        /// </summary>
        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
    }
}