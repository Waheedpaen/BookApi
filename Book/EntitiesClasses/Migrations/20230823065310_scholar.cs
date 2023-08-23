using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntitiesClasses.Migrations
{
    /// <inheritdoc />
    public partial class scholar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scholars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MadrassaName = table.Column<string>(type: "varchar(100)", nullable: false),
                    FarqaCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(name: "Updated_At", type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scholars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scholars_FarqaCategories_FarqaCategoryId",
                        column: x => x.FarqaCategoryId,
                        principalTable: "FarqaCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scholars_FarqaCategoryId",
                table: "Scholars",
                column: "FarqaCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scholars");
        }
    }
}
