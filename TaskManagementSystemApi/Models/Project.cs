namespace TaskManagementSystemApi.Models
{
    /// <summary>
    /// 專案 Entity
    /// 對應資料表：project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 專案主鍵 ID
        /// 對應欄位：project_id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 專案名稱
        /// 對應欄位：project_name
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 專案描述
        /// 對應欄位：project_description
        /// </summary>
        public string? ProjectDescription { get; set; }

        /// <summary>
        /// 建立此專案的使用者 ID
        /// 對應欄位：created_by_user_id
        /// </summary>
        public int CreatedByUserId { get; set; }

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
        /// 建立此專案的使用者
        /// </summary>
        public User CreatedByUser { get; set; } = null!;

        /// <summary>
        /// 此專案底下的任務集合
        /// </summary>
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}