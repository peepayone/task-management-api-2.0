using Microsoft.EntityFrameworkCore;
using TaskManagementSystemApi.Data.Configurations;
using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data
{
    /// <summary>
    /// 系統主要 DbContext
    /// 負責 Entity 與資料表之間的對應。
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// AppDbContext 建構式
        /// </summary>
        /// <param name="options">EF Core DbContext 設定選項</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DbSets
        // =========================

        /// <summary>
        /// 使用者資料集
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 專案資料集
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// 任務資料集
        /// 注意：Entity 名稱為 TaskItem，避免與 System.Threading.Tasks.Task 衝突。
        /// </summary>
        public DbSet<TaskItem> Tasks { get; set; }

        /// <summary>
        /// 任務留言資料集
        /// </summary>
        public DbSet<TaskComment> TaskComments { get; set; }

        /// <summary>
        /// 套用所有 Entity 的 Fluent API 設定
        /// </summary>
        /// <param name="modelBuilder">EF Core ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 套用各 Entity Configuration
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
            modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
        }
    }
}