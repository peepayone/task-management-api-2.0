using TaskManagementSystemApi.DTOs.Task;
using TaskManagementSystemApi.Queries;

namespace TaskManagementSystemApi.Services.Interfaces
{
    /// <summary>
    /// 任務服務介面
    /// 定義任務相關的商業邏輯。
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// 取得任務清單，並支援篩選與排序
        /// </summary>
        /// <param name="queryParameters">任務查詢參數</param>
        /// <returns>任務 DTO 清單</returns>
        IEnumerable<TaskDto> GetAllTasks(TaskQueryParameters queryParamters);

        /// <summary>
        /// 依照任務 ID 取得單一任務
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <returns>若存在則回傳 TaskDto，否則回傳 null</returns>
        TaskDto? GetTaskById(int taskId);

        /// <summary>
        /// 建立新任務
        /// </summary>
        /// <param name="createTaskDto">建立任務 DTO</param>
        /// <returns>建立完成後的任務 DTO</returns>
        TaskDto CreateTask(CreateTaskDto createTaskDto);

        /// <summary>
        /// 更新任務
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <param name="updateTaskDto">更新任務 DTO</param>
        /// <returns>更新成功回傳 true，找不到任務回傳 false</returns>
        bool UpdateTask(int taskId, UpdateTaskDto updateTaskDto);

        /// <summary>
        /// 刪除任務
        /// </summary>
        /// <param name="taskId">任務 ID</param>
        /// <returns>刪除成功回傳 true，找不到任務回傳 false</returns>
        bool DeleteTask(int taskId);
    }
}