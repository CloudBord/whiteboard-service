using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whiteboard.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardContentsToBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoardContents",
                table: "Boards",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardContents",
                table: "Boards");
        }
    }
}
