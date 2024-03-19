using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendantIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_token_users_UserId",
                table: "refresh_token");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "refresh_token",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AttendantId",
                table: "refresh_token",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_token_users_UserId",
                table: "refresh_token",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_token_users_UserId",
                table: "refresh_token");

            migrationBuilder.DropColumn(
                name: "AttendantId",
                table: "refresh_token");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "refresh_token",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_token_users_UserId",
                table: "refresh_token",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
