using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data.Configurations
{
    /// <summary>
    /// Project Entity 的 Fluent API 設定
    /// </summary>
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // 設定資料表名稱
            builder.ToTable("project");

            // 設定主鍵
            builder.HasKey(project => project.ProjectId)
                   .HasName("pk_project");

            // 專案主鍵 ID
            builder.Property(project => project.ProjectId)
                   .HasColumnName("project_id")
                   .ValueGeneratedOnAdd();

            // 專案名稱
            builder.Property(project => project.ProjectName)
                   .HasColumnName("project_name")
                   .HasMaxLength(150)
                   .IsRequired();

            // 專案描述
            builder.Property(project => project.ProjectDescription)
                   .HasColumnName("project_description")
                   .HasMaxLength(1000);

            // 建立者使用者 ID
            builder.Property(project => project.CreatedByUserId)
                   .HasColumnName("created_by_user_id")
                   .IsRequired();

            // 建立時間
            builder.Property(project => project.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 更新時間
            builder.Property(project => project.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 關聯：Project -> User (CreatedByUser)
            builder.HasOne(project => project.CreatedByUser)
                   .WithMany(user => user.CreatedProjects)
                   .HasForeignKey(project => project.CreatedByUserId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("fk_project_created_by_user");

            // 索引：created_by_user_id
            builder.HasIndex(project => project.CreatedByUserId)
                   .HasDatabaseName("ix_project_created_by_user_id");

            // 索引：project_name
            builder.HasIndex(project => project.ProjectName)
                   .HasDatabaseName("ix_project_project_name");
        }
    }
}