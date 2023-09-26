using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntitiesClasses.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MadrassaClassId",
                table: "MadrassaBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MadrassaBooks_MadrassaClassId",
                table: "MadrassaBooks",
                column: "MadrassaClassId");

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
                name: "FK_MadrassaBooks_MadrassaClasses_MadrassaClassId",
                table: "MadrassaBooks");

            migrationBuilder.DropIndex(
                name: "IX_MadrassaBooks_MadrassaClassId",
                table: "MadrassaBooks");

            migrationBuilder.DropColumn(
                name: "MadrassaClassId",
                table: "MadrassaBooks");
        }
    }
}
