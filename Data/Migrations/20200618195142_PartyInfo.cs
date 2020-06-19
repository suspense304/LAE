using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class PartyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartySize",
                table: "Actvity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PartyInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<string>(nullable: false),
                    StartingTime = table.Column<DateTime>(nullable: false),
                    PartySize = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    ServerName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyInfo_Actvity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Actvity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(nullable: false),
                    InfoId = table.Column<int>(nullable: true),
                    PartyNameId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Party_PartyInfo_InfoId",
                        column: x => x.InfoId,
                        principalTable: "PartyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Party_AspNetUsers_PartyNameId",
                        column: x => x.PartyNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Party_InfoId",
                table: "Party",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_PartyNameId",
                table: "Party",
                column: "PartyNameId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyInfo_ActivityId",
                table: "PartyInfo",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyInfo_CreatedById",
                table: "PartyInfo",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "PartyInfo");

            migrationBuilder.DropColumn(
                name: "PartySize",
                table: "Actvity");
        }
    }
}
