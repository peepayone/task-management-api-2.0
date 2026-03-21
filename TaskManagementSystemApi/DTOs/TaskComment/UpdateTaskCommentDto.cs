using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystemApi.DTOs.TaskComment
{
    /// <summary>
    /// 更新任務留言 DTO
    /// </summary>
    public class UpdateTaskCommentDto
    {
        /// <summary>
        /// 留言內容
        /// </summary>
        [Required]
        [MaxLength(2000)]
        public string TaskCommentContent { get; set; } = string.Empty;
    }
}