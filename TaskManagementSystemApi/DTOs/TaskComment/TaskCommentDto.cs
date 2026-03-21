namespace TaskManagementSystemApi.DTOs.TaskComment
{
    /// <summary>
    /// 任務留言回傳 DTO
    /// </summary>
    public class TaskCommentDto
    {
        /// <summary>
        /// 留言 ID
        /// </summary>
        public int TaskCommentId { get; set; }

        /// <summary>
        /// 所屬任務 ID
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 留言者使用者 ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 留言內容
        /// </summary>
        public string TaskCommentContent { get; set; } = string.Empty;
    }
}