namespace TaskManagementSystemApi.Models
{
    /// <summary>
    /// 使用者 Entity
    /// 對應資料表：user
    /// </summary>
    public class User
    {
        /// <summary>
        /// 使用者主鍵 ID
        /// 對應欄位：user_id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 使用者名稱
        /// 對應欄位：user_name
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 使用者 Email
        /// 對應欄位：user_email
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;

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
        /// 此使用者建立的專案集合
        /// </summary>
        public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();

        /// <summary>
        /// 此使用者建立的任務集合
        /// </summary>
        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();

        /// <summary>
        /// 此使用者被指派的任務集合
        /// </summary>
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

        /// <summary>
        /// 此使用者撰寫的任務留言集合
        /// </summary>
        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
    }
}