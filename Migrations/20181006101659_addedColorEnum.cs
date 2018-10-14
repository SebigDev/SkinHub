using Microsoft.EntityFrameworkCore.Migrations;

namespace SkinHubApp.Migrations
{
    public partial class addedColorEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_ColorType_ColorTypeID",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_ColorTypeID",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "ColorTypeID",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Member",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "ColorTypeID",
                table: "Member",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Member_ColorTypeID",
                table: "Member",
                column: "ColorTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_ColorType_ColorTypeID",
                table: "Member",
                column: "ColorTypeID",
                principalTable: "ColorType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
