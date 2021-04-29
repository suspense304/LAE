using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class RemoveEventInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MemberFourId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MemberThreeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MemberTwoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServerName = table.Column<int>(type: "int", nullable: false),
                    StartingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventInfo_Actvity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Actvity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventInfo_AspNetUsers_MemberFourId",
                        column: x => x.MemberFourId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventInfo_AspNetUsers_MemberThreeId",
                        column: x => x.MemberThreeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventInfo_AspNetUsers_MemberTwoId",
                        column: x => x.MemberTwoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_ActivityId",
                table: "EventInfo",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_CreatedById",
                table: "EventInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_MemberFourId",
                table: "EventInfo",
                column: "MemberFourId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_MemberThreeId",
                table: "EventInfo",
                column: "MemberThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_MemberTwoId",
                table: "EventInfo",
                column: "MemberTwoId");
        }
    }
}
