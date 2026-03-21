using TaskManagementSystemApi.DTOs.User;

namespace TaskManagementSystemApi.Services.Interfaces
{
    /// <summary>
    /// 使用者服務介面
    /// 定義與使用者相關的商業邏輯操作。
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <returns>使用者 DTO 清單</returns>
        IEnumerable<UserDto> GetAllUsers();

        /// <summary>
        /// 依照使用者 ID 取得單一使用者
        /// </summary>
        /// <param name="userId">使用者 ID</param>
        /// <returns>若存在則回傳 UserDto，否則回傳 null</returns>
        UserDto? GetUserById(int userId);
    }
}