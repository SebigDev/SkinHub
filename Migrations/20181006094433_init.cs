using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkinHubApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenderType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ColorType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    GenderTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ColorType_GenderType_GenderTypeID",
                        column: x => x.GenderTypeID,
                        principalTable: "GenderType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Middlename = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    ColorTypeID = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Member_ColorType_ColorTypeID",
                        column: x => x.ColorTypeID,
                        principalTable: "ColorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ColorTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductType_ColorType_ColorTypeID",
                        column: x => x.ColorTypeID,
                        principalTable: "ColorType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductListType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProductTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductListType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductListType_ProductType_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ProductListTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Post_ProductListType_ProductListTypeID",
                        column: x => x.ProductListTypeID,
                        principalTable: "ProductListType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentBody = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    PostID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReplyBody = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    CommentID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reply_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorType_GenderTypeID",
                table: "ColorType",
                column: "GenderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostID",
                table: "Comment",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ColorTypeID",
                table: "Member",
                column: "ColorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_ProductListTypeID",
                table: "Post",
                column: "ProductListTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductListType_ProductTypeID",
                table: "ProductListType",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_ColorTypeID",
                table: "ProductType",
                column: "ColorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_CommentID",
                table: "Reply",
                column: "CommentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "ProductListType");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "ColorType");

            migrationBuilder.DropTable(
                name: "GenderType");
        }
    }
}
