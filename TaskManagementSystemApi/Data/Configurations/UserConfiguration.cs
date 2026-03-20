using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Data.Configurations
{
    /// <summary>
    /// User Entity 的 Fluent API 設定
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // 設定資料表名稱
            builder.ToTable("user");

            // 設定主鍵
            builder.HasKey(user => user.UserId)
                   .HasName("pk_user");

            // 設定主鍵欄位對應與 Identity
            builder.Property(user => user.UserId)
                   .HasColumnName("user_id")
                   .ValueGeneratedOnAdd();

            // 使用者名稱
            builder.Property(user => user.UserName)
                   .HasColumnName("user_name")
                   .HasMaxLength(100)
                   .IsRequired();

            // 使用者 Email
            builder.Property(user => user.UserEmail)
                   .HasColumnName("user_email")
                   .HasMaxLength(255)
                   .IsRequired();

            // 建立時間
            builder.Property(user => user.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // 更新時間
            builder.Property(user => user.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIME()")
                   .IsRequired();

            // Unique constraint：user_email
            builder.HasIndex(user => user.UserEmail)
                   .IsUnique()
                   .HasDatabaseName("uq_user_user_email");
        }
    }
}