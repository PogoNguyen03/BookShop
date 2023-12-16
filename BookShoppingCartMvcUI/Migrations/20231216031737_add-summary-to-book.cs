using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShoppingCartMvcUI.Migrations
{
    /// <inheritdoc />
    public partial class addsummarytobook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Book",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Book");
        }
    }
}
