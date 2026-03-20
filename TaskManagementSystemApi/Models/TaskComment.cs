namespace TaskManagementSystemApi.Models
{
    /// <summary>
    /// 任務留言 Entity
    /// 對應資料表：task_comment
    /// </summary>
    public class TaskComment
    {
        /// <summary>
        /// 任務留言主鍵 ID
        /// 對應欄位：task_comment_id
        /// </summary>
        public int TaskCommentId { get; set; }

        /// <summary>
        /// 所屬任務 ID
        /// 對應欄位：task_id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 留言者使用者 ID
        /// 對應欄位：user_id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 留言內容
        /// 對應欄位：task_comment_content
        /// </summary>
        public string TaskCommentContent { get; set; } = string.Empty;

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
        /// 此留言所屬任務
        /// </summary>
        public TaskItem Task { get; set; } = null!;

        /// <summary>
        /// 此留言的使用者
        /// </summary>
        public User User { get; set; } = null!;
    }
}