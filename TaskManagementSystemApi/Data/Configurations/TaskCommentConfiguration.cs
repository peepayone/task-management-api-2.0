using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data.Configurations
{
    /// <summary>
    /// TaskComment Entity 的 Fluent API 設定
    /// </summary>
    public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            // 設定資料表名稱
            builder.ToTable("task_comment");

            // 設定主鍵
            builder.HasKey(taskComment => taskComment.TaskCommentId)
                   .HasName("pk_task_comment");

            // 留言主鍵 ID
            builder.Property(taskComment => taskComment.TaskCommentId)
                   .HasColumnName("task_comment_id")
                   .ValueGeneratedOnAdd();

            // 所屬任務 ID
            builder.Property(taskComment => taskComment.TaskId)
                   .HasColumnName("task_id")
                   .IsRequired();

            // 留言者使用者 ID
            builder.Property(taskComment => taskComment.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();

            // 留言內容
            builder.Property(taskComment => taskComment.TaskCommentContent)
                   .HasColumnName("task_comment_content")
                   .HasMaxLength(2000)
                   .IsRequired();

            // 建立時間
            builder.Property(taskComment => taskComment.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 更新時間
            builder.Property(taskComment => taskComment.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 關聯：TaskComment -> Task
            builder.HasOne(taskComment => taskComment.Task)
                   .WithMany(task => task.TaskComments)
                   .HasForeignKey(taskComment => taskComment.TaskId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("fk_task_comment_task");

            // 關聯：TaskComment -> User
            builder.HasOne(taskComment => taskComment.User)
                   .WithMany(user => user.TaskComments)
                   .HasForeignKey(taskComment => taskComment.UserId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("fk_task_comment_user");

            // 索引：task_id
            builder.HasIndex(taskComment => taskComment.TaskId)
                   .HasDatabaseName("ix_task_comment_task_id");

            // 索引：user_id
            builder.HasIndex(taskComment => taskComment.UserId)
                   .HasDatabaseName("ix_task_comment_user_id");
        }
    }
}