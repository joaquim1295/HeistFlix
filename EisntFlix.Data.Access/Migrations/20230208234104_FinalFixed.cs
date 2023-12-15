using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EisntFlix.Data.Access.Migrations
{
    public partial class FinalFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Streamings_StreamingId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Streamings_StreamingId",
                table: "Series");

            migrationBuilder.AlterColumn<int>(
                name: "StreamingId",
                table: "Series",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StreamingId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Streamings_StreamingId",
                table: "Movies",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Streamings_StreamingId",
                table: "Series",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Streamings_StreamingId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Streamings_StreamingId",
                table: "Series");

            migrationBuilder.AlterColumn<int>(
                name: "StreamingId",
                table: "Series",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StreamingId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Streamings_StreamingId",
                table: "Movies",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Streamings_StreamingId",
                table: "Series",
                column: "StreamingId",
                principalTable: "Streamings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
