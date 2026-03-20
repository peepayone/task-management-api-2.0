using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data.Seed
{
    /// <summary>
    /// 資料庫初始化資料（Seed Data）
    /// </summary>
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // 如果已經有資料，就不再新增
            if (context.Users.Any())
                return;

            // =========================
            // Users
            // =========================
            var james = new User
            {
                UserName = "James",
                UserEmail = "james@example.com"
            };

            var alice = new User
            {
                UserName = "Alice",
                UserEmail = "alice@example.com"
            };

            var david = new User
            {
                UserName = "David",
                UserEmail = "david@example.com"
            };

            context.Users.AddRange(james, alice, david);
            context.SaveChanges();

            // =========================
            // Projects
            // =========================
            var energyProject = new Project
            {
                ProjectName = "Energy Management System",
                ProjectDescription = "Energy system",
                CreatedByUserId = james.UserId
            };

            var healthProject = new Project
            {
                ProjectName = "Health Management System",
                ProjectDescription = "Health system",
                CreatedByUserId = alice.UserId
            };

            context.Projects.AddRange(energyProject, healthProject);
            context.SaveChanges();

            // =========================
            // Tasks
            // =========================
            var task1 = new TaskItem
            {
                ProjectId = energyProject.ProjectId,
                TaskTitle = "Design DB",
                TaskStatus = "done",
                CreatedByUserId = james.UserId,
                AssignedToUserId = james.UserId,
                DueDate = DateTime.Now.AddDays(-3)
            };

            var task2 = new TaskItem
            {
                ProjectId = energyProject.ProjectId,
                TaskTitle = "Build API",
                TaskStatus = "doing",
                TaskDescription = "Develop RESTful APIs for task management, including CRUD operations, filtering, and integration with the database.",
                CreatedByUserId = james.UserId,
                AssignedToUserId = david.UserId,
                DueDate = DateTime.Now.AddDays(2)
            };

            var task3 = new TaskItem
            {
                ProjectId = healthProject.ProjectId,
                TaskTitle = "User Login",
                TaskStatus = "todo",
                TaskDescription = "Allow users to securely log into the system.",
                CreatedByUserId = alice.UserId,
                AssignedToUserId = james.UserId
            };

            context.Tasks.AddRange(task1, task2, task3);
            context.SaveChanges();

            // =========================
            // Comments
            // =========================
            var comment1 = new TaskComment
            {
                TaskId = task2.TaskId,
                UserId = james.UserId,
                TaskCommentContent = "API structure looks good"
            };

            var comment2 = new TaskComment
            {
                TaskId = task2.TaskId,
                UserId = david.UserId,
                TaskCommentContent = "Working on it"
            };

            context.TaskComments.AddRange(comment1, comment2);
            context.SaveChanges();
        }
    }
}