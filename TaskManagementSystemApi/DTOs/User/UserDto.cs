namespace TaskManagementSystemApi.DTOs.User
{
    /// <summary>
    /// 使用者資料傳輸物件（DTO）
    /// 用於 API 回傳資料。
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 使用者 ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 使用者 Email
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;
    }
}