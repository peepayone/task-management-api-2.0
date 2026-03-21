using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadeDeleteForProjectAndTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_task_project",
                table: "task");

            migrationBuilder.DropForeignKey(
                name: "fk_task_comment_task",
                table: "task_comment");

            migrationBuilder.AddForeignKey(
                name: "fk_task_project",
                table: "task",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_task_comment_task",
                table: "task_comment",
                column: "task_id",
                principalTable: "task",
                principalColumn: "task_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_task_project",
                table: "task");

            migrationBuilder.DropForeignKey(
                name: "fk_task_comment_task",
                table: "task_comment");

            migrationBuilder.AddForeignKey(
                name: "fk_task_project",
                table: "task",
                column: "project_id",
                principalTable: "project",
                principalColumn: "project_id");

            migrationBuilder.AddForeignKey(
                name: "fk_task_comment_task",
                table: "task_comment",
                column: "task_id",
                principalTable: "task",
                principalColumn: "task_id");
        }
    }
}
