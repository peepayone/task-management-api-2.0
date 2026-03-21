using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystemApi.DTOs.TaskComment
{
    /// <summary>
    /// 建立任務留言 DTO
    /// </summary>
    public class CreateTaskCommentDto
    {
        /// <summary>
        /// 留言者使用者 ID
        /// 實務上可由目前使用者帶入
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 留言內容
        /// </summary>
        [Required]
        [MaxLength(2000)]
        public string TaskCommentContent { get; set; } = string.Empty;
    }
}