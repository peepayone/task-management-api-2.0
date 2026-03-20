using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    user_email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    project_description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    created_by_user_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project", x => x.project_id);
                    table.ForeignKey(
                        name: "fk_project_created_by_user",
                        column: x => x.created_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    task_title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    task_description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    task_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    assigned_to_user_id = table.Column<int>(type: "int", nullable: true),
                    created_by_user_id = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task", x => x.task_id);
                    table.CheckConstraint("ck_task_task_status", "task_status IN ('todo', 'doing', 'done')");
                    table.ForeignKey(
                        name: "fk_task_assigned_to_user",
                        column: x => x.assigned_to_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_task_created_by_user",
                        column: x => x.created_by_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "fk_task_project",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id");
                });

            migrationBuilder.CreateTable(
                name: "task_comment",
                columns: table => new
                {
                    task_comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    task_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    task_comment_content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_comment", x => x.task_comment_id);
                    table.ForeignKey(
                        name: "fk_task_comment_task",
                        column: x => x.task_id,
                        principalTable: "task",
                        principalColumn: "task_id");
                    table.ForeignKey(
                        name: "fk_task_comment_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_created_by_user_id",
                table: "project",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_project_name",
                table: "project",
                column: "project_name");

            migrationBuilder.CreateIndex(
                name: "ix_task_assigned_to_user_id",
                table: "task",
                column: "assigned_to_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_created_by_user_id",
                table: "task",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_due_date",
                table: "task",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "ix_task_project_id",
                table: "task",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_task_status",
                table: "task",
                column: "task_status");

            migrationBuilder.CreateIndex(
                name: "ix_task_comment_task_id",
                table: "task_comment",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_comment_user_id",
                table: "task_comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "uq_user_user_email",
                table: "user",
                column: "user_email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_comment");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
