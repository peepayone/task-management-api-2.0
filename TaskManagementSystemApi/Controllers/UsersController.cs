using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemApi.Services.Interfaces;

namespace TaskManagementSystemApi.Controllers
{
    /// <summary>
    /// 使用者 API 控制器
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 建構式注入使用者服務
        /// </summary>
        /// <param name="userService">使用者服務</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 取得所有使用者
        /// GET: /api/users
        /// </summary>
        /// <returns>使用者清單</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        /// <summary>
        /// 依照使用者 ID 取得單一使用者
        /// GET: /api/users/{id}
        /// </summary>
        /// <param name="id">使用者 ID</param>
        /// <returns>單一使用者資料</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
