using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntitiesClasses.Migrations
{
    /// <inheritdoc />
    public partial class datamonthly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileNamePDF",
                table: "MonthlyMagzines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePathPDF",
                table: "MonthlyMagzines",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNamePDF",
                table: "MonthlyMagzines");

            migrationBuilder.DropColumn(
                name: "FilePathPDF",
                table: "MonthlyMagzines");
        }
    }
}
