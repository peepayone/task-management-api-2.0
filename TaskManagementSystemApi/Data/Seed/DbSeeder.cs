using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data.Seed
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            // =========================
            // 0. 如果已有資料就不重複塞
            // =========================
            if (dbContext.Users.Any()) return;

            // =========================
            // 1. Users
            // =========================
            var users = new List<User>
            {
                new User { UserName = "James", UserEmail = "james@test.com", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new User { UserName = "Alice", UserEmail = "alice@test.com", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new User { UserName = "Michael", UserEmail = "michael@test.com", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new User { UserName = "David", UserEmail = "david@test.com", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            };

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            // =========================
            // 2. Projects
            // =========================
            var projects = new List<Project>
            {
                new Project
                {
                    ProjectName = "Energy Management System",
                    ProjectDescription = "Monitor energy usage and analytics",
                    CreatedByUserId = users[0].UserId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Project
                {
                    ProjectName = "Health Management System",
                    ProjectDescription = "Manage patient records and appointments",
                    CreatedByUserId = users[1].UserId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Project
                {
                    ProjectName = "Inventory Management System",
                    ProjectDescription = "Track stock and warehouse operations",
                    CreatedByUserId = users[2].UserId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            dbContext.Projects.AddRange(projects);
            dbContext.SaveChanges();

            // =========================
            // 3. Tasks（TaskItem）
            // =========================
            var tasks = new List<TaskItem>
            {
                // ===== Project 1 =====
                new TaskItem
                {
                    ProjectId = projects[0].ProjectId,
                    TaskTitle = "Design dashboard UI",
                    TaskDescription = "Initial layout for energy dashboard",
                    TaskStatus = "todo",
                    AssignedToUserId = users[0].UserId,
                    CreatedByUserId = users[0].UserId,
                    DueDate = DateTime.Today.AddDays(7),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskItem
                {
                    ProjectId = projects[0].ProjectId,
                    TaskTitle = "Implement energy API",
                    TaskDescription = "Provide usage data endpoint",
                    TaskStatus = "doing",
                    AssignedToUserId = users[1].UserId,
                    CreatedByUserId = users[0].UserId,
                    DueDate = DateTime.Today.AddDays(5),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskItem
                {
                    ProjectId = projects[0].ProjectId,
                    TaskTitle = "Export report feature",
                    TaskDescription = "Allow CSV export",
                    TaskStatus = "done",
                    AssignedToUserId = users[2].UserId,
                    CreatedByUserId = users[1].UserId,
                    DueDate = DateTime.Today.AddDays(-2),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },

                // ===== Project 2 =====
                new TaskItem
                {
                    ProjectId = projects[1].ProjectId,
                    TaskTitle = "Build patient list page",
                    TaskDescription = "Display patient records",
                    TaskStatus = "todo",
                    AssignedToUserId = users[1].UserId,
                    CreatedByUserId = users[1].UserId,
                    DueDate = DateTime.Today.AddDays(10),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskItem
                {
                    ProjectId = projects[1].ProjectId,
                    TaskTitle = "Integrate appointment module",
                    TaskDescription = "Connect scheduling API",
                    TaskStatus = "doing",
                    AssignedToUserId = users[3].UserId,
                    CreatedByUserId = users[1].UserId,
                    DueDate = DateTime.Today.AddDays(6),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskItem
                {
                    ProjectId = projects[1].ProjectId,
                    TaskTitle = "Fix validation bug",
                    TaskDescription = "Resolve form validation issue",
                    TaskStatus = "done",
                    AssignedToUserId = users[0].UserId,
                    CreatedByUserId = users[2].UserId,
                    DueDate = DateTime.Today.AddDays(-1),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },

                // ===== Project 3 =====
                new TaskItem
                {
                    ProjectId = projects[2].ProjectId,
                    TaskTitle = "Stock summary widget",
                    TaskDescription = "Display inventory summary",
                    TaskStatus = "todo",
                    AssignedToUserId = users[2].UserId,
                    CreatedByUserId = users[2].UserId,
                    DueDate = DateTime.Today.AddDays(8),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskItem
                {
                    ProjectId = projects[2].ProjectId,
                    TaskTitle = "Warehouse transfer API",
                    TaskDescription = "Implement stock transfer logic",
                    TaskStatus = "doing",
                    AssignedToUserId = users[3].UserId,
                    CreatedByUserId = users[2].UserId,
                    DueDate = DateTime.Today.AddDays(4),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskItem
                {
                    ProjectId = projects[2].ProjectId,
                    TaskTitle = "Order status screen",
                    TaskDescription = "Finalize UI behavior",
                    TaskStatus = "done",
                    AssignedToUserId = users[0].UserId,
                    CreatedByUserId = users[3].UserId,
                    DueDate = DateTime.Today.AddDays(-3),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            dbContext.Tasks.AddRange(tasks);
            dbContext.SaveChanges();

            // =========================
            // 4. Comments（部分 task 沒 comment）
            // =========================
            var comments = new List<TaskComment>
            {
                new TaskComment
                {
                    TaskId = tasks[0].TaskId,
                    UserId = users[0].UserId,
                    TaskCommentContent = "Initial UI draft completed.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskComment
                {
                    TaskId = tasks[1].TaskId,
                    UserId = users[1].UserId,
                    TaskCommentContent = "API still under development.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskComment
                {
                    TaskId = tasks[1].TaskId,
                    UserId = users[0].UserId,
                    TaskCommentContent = "Make sure response uses snake_case.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskComment
                {
                    TaskId = tasks[4].TaskId,
                    UserId = users[3].UserId,
                    TaskCommentContent = "UI integration in progress.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TaskComment
                {
                    TaskId = tasks[7].TaskId,
                    UserId = users[3].UserId,
                    TaskCommentContent = "Transfer logic implemented.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            dbContext.TaskComments.AddRange(comments);
            dbContext.SaveChanges();
        }
    }
}