using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.DTOs.User;
using TaskManagementSystemApi.Services.Interfaces;

namespace TaskManagementSystemApi.Services
{
    /// <summary>
    /// 使用者服務實作
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 建構式注入 AppDbContext
        /// </summary>
        /// <param name="dbContext">資料庫內容物件</param>
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <returns>使用者 DTO 清單</returns>
        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users
                .Select(user => new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail
                }).ToList();

            return users;
        }

        /// <summary>
        /// 依照使用者 ID 取得單一使用者
        /// </summary>
        /// <param name="userId">使用者 ID</param>
        /// <returns>若存在則回傳 UserDto，否則回傳 null</returns>
        public UserDto? GetUserById(int userId)
        {
            var user = _dbContext.Users
                .Where(user => user.UserId == userId)
                .Select(user => new UserDto 
                { 
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail= user.UserEmail
                })
                .FirstOrDefault();

            return user;
        }
    }
}
