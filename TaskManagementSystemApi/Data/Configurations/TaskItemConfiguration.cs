using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data.Configurations
{
    /// <summary>
    /// TaskItem Entity 的 Fluent API 設定
    /// </summary>
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            // 設定資料表名稱
            builder.ToTable("task");

            // 設定主鍵
            builder.HasKey(task => task.TaskId)
                   .HasName("pk_task");

            // 任務主鍵 ID
            builder.Property(task => task.TaskId)
                   .HasColumnName("task_id")
                   .ValueGeneratedOnAdd();

            // 所屬專案 ID
            builder.Property(task => task.ProjectId)
                   .HasColumnName("project_id")
                   .IsRequired();

            // 任務標題
            builder.Property(task => task.TaskTitle)
                   .HasColumnName("task_title")
                   .HasMaxLength(200)
                   .IsRequired();

            // 任務描述
            builder.Property(task => task.TaskDescription)
                   .HasColumnName("task_description")
                   .HasMaxLength(2000);

            // 任務狀態
            builder.Property(task => task.TaskStatus)
                   .HasColumnName("task_status")
                   .HasMaxLength(20)
                   .IsRequired();

            // 被指派者使用者 ID
            builder.Property(task => task.AssignedToUserId)
                   .HasColumnName("assigned_to_user_id");

            // 建立者使用者 ID
            builder.Property(task => task.CreatedByUserId)
                   .HasColumnName("created_by_user_id")
                   .IsRequired();

            // 截止日期
            builder.Property(task => task.DueDate)
                   .HasColumnName("due_date");

            // 建立時間
            builder.Property(task => task.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 更新時間
            builder.Property(task => task.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 關聯：Task -> Project
            builder.HasOne(task => task.Project)
                   .WithMany(project => project.Tasks)
                   .HasForeignKey(task => task.ProjectId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("fk_task_project");

            // 關聯：Task -> User (CreatedByUser)
            builder.HasOne(task => task.CreatedByUser)
                   .WithMany(user => user.CreatedTasks)
                   .HasForeignKey(task => task.CreatedByUserId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("fk_task_created_by_user");

            // 關聯：Task -> User (AssignedToUser)
            builder.HasOne(task => task.AssignedToUser)
                   .WithMany(user => user.AssignedTasks)
                   .HasForeignKey(task => task.AssignedToUserId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("fk_task_assigned_to_user");

            // CHECK constraint：task_status 限制值
            builder.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "ck_task_task_status",
                    "task_status IN ('todo', 'doing', 'done')"
                );
            });

            // 索引：project_id
            builder.HasIndex(task => task.ProjectId)
                   .HasDatabaseName("ix_task_project_id");

            // 索引：assigned_to_user_id
            builder.HasIndex(task => task.AssignedToUserId)
                   .HasDatabaseName("ix_task_assigned_to_user_id");

            // 索引：created_by_user_id
            builder.HasIndex(task => task.CreatedByUserId)
                   .HasDatabaseName("ix_task_created_by_user_id");

            // 索引：task_status
            builder.HasIndex(task => task.TaskStatus)
                   .HasDatabaseName("ix_task_task_status");

            // 索引：due_date
            builder.HasIndex(task => task.DueDate)
                   .HasDatabaseName("ix_task_due_date");
        }
    }
}