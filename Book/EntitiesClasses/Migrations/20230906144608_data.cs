using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntitiesClasses.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookImages_BookDetails_BookDetailId",
                table: "BookImages");

            migrationBuilder.AlterColumn<int>(
                name: "BookDetailId",
                table: "BookImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FilePathImageUrl",
                table: "BookDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlName",
                table: "BookDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookImages_BookDetails_BookDetailId",
                table: "BookImages",
                column: "BookDetailId",
                principalTable: "BookDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookImages_BookDetails_BookDetailId",
                table: "BookImages");

            migrationBuilder.DropColumn(
                name: "FilePathImageUrl",
                table: "BookDetails");

            migrationBuilder.DropColumn(
                name: "ImageUrlName",
                table: "BookDetails");

            migrationBuilder.AlterColumn<int>(
                name: "BookDetailId",
                table: "BookImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookImages_BookDetails_BookDetailId",
                table: "BookImages",
                column: "BookDetailId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
