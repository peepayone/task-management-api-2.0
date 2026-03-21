using TaskManagementSystemApi.DTOs.TaskComment;

namespace TaskManagementSystemApi.Services.Interfaces
{
    /// <summary>
    /// 任務留言服務介面
    /// 定義任務留言相關的商業邏輯。
    /// </summary>
    public interface ITaskCommentService
    {
        /// <summary>
        /// 依照任務 ID 取得該任務的所有留言
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <returns>任務留言 DTO 清單</returns>
        IEnumerable<TaskCommentDto> GetCommentsByTaskId(int taskId);

        /// <summary>
        /// 建立新的任務留言
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <param name="createTaskCommentDto">建立留言 DTO</param>
        /// <returns>建立完成後的留言 DTO；若 task 不存在則回傳 null</returns>
        TaskCommentDto? CreateTaskComment(int taskId, CreateTaskCommentDto createTaskCommentDto);

        /// <summary>
        /// 更新任務留言
        /// </summary>
        /// <param name="taskCommentId">留言 ID</param>
        /// <param name="updateTaskCommentDto">更新留言 DTO</param>
        /// <returns>更新成功回傳 true，找不到留言回傳 false</returns>
        bool UpdateTaskComment(int taskCommentId, UpdateTaskCommentDto updateTaskCommentDto);

        /// <summary>
        /// 刪除任務留言
        /// </summary>
        /// <param name="taskCommentId">留言 ID</param>
        /// <returns>刪除成功回傳 true，找不到留言回傳 false</returns>
        bool DeleteTaskComment(int taskCommentId);
    }
}