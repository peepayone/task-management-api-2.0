namespace TaskManagementSystemApi.Queries
{
    /// <summary>
    /// 任務查詢參數
    /// 用於接收 /api/tasks 的篩選與排序條件。
    /// </summary>
    public class TaskQueryParameters
    {
        /// <summary>
        /// 依任務狀態篩選，例如：todo / doing / done
        /// </summary>
        public string? TaskStatus { get; set; }

        /// <summary>
        /// 依被指派使用者 ID 篩選
        /// </summary>
        public int? AssignedToUserId { get; set; }

        /// <summary>
        /// 依專案 ID 篩選
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 排序欄位，例如：due_date / created_at
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// 排序方向，例如：asc / desc
        /// </summary>
        public string? SortOrder { get; set; }
    }
}