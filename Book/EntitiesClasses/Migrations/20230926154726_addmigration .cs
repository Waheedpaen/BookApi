using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntitiesClasses.Migrations
{
    /// <inheritdoc />
    public partial class addmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MadrassaBookCatgories_MadrassaBooks_MadrassaBookId",
                table: "MadrassaBookCatgories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Scholars",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MadrassaBooks",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<int>(
                name: "MadrassaClassId",
                table: "MadrassaBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MadrassaBookCatgories",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<int>(
                name: "MadrassaBookId",
                table: "MadrassaBookCatgories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FarqaCategories",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookImages",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookDetails",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookCategories",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.CreateTable(
                name: "MadrassaClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(name: "Updated_At", type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MadrassaClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyMagzines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(name: "Updated_At", type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyMagzines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MadrassaBooks_MadrassaClassId",
                table: "MadrassaBooks",
                column: "MadrassaClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_MadrassaBookCatgories_MadrassaBooks_MadrassaBookId",
                table: "MadrassaBookCatgories",
                column: "MadrassaBookId",
                principalTable: "MadrassaBooks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MadrassaBooks_MadrassaClasses_MadrassaClassId",
                table: "MadrassaBooks",
                column: "MadrassaClassId",
                principalTable: "MadrassaClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MadrassaBookCatgories_MadrassaBooks_MadrassaBookId",
                table: "MadrassaBookCatgories");

            migrationBuilder.DropForeignKey(
                name: "FK_MadrassaBooks_MadrassaClasses_MadrassaClassId",
                table: "MadrassaBooks");

            migrationBuilder.DropTable(
                name: "MadrassaClasses");

            migrationBuilder.DropTable(
                name: "MonthlyMagzines");

            migrationBuilder.DropIndex(
                name: "IX_MadrassaBooks_MadrassaClassId",
                table: "MadrassaBooks");

            migrationBuilder.DropColumn(
                name: "MadrassaClassId",
                table: "MadrassaBooks");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Scholars",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MadrassaBooks",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MadrassaBookCatgories",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<int>(
                name: "MadrassaBookId",
                table: "MadrassaBookCatgories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FarqaCategories",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookImages",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookDetails",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BookCategories",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AddForeignKey(
                name: "FK_MadrassaBookCatgories_MadrassaBooks_MadrassaBookId",
                table: "MadrassaBookCatgories",
                column: "MadrassaBookId",
                principalTable: "MadrassaBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
