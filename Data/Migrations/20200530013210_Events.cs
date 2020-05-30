using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "EventInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    MemberTwoId = table.Column<string>(nullable: true),
                    MemberThreeId = table.Column<string>(nullable: true),
                    MemberFourId = table.Column<string>(nullable: true),
                    StartingTime = table.Column<DateTime>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Actvity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    EventTypeTypeId = table.Column<int>(nullable: true),
                    MinGearScore = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    EventInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actvity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actvity_EventInfo_EventInfoId",
                        column: x => x.EventInfoId,
                        principalTable: "EventInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actvity_EventType_EventTypeTypeId",
                        column: x => x.EventTypeTypeId,
                        principalTable: "EventType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actvity_EventInfoId",
                table: "Actvity",
                column: "EventInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Actvity_EventTypeTypeId",
                table: "Actvity",
                column: "EventTypeTypeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_EventInfo_Actvity_ActivityId",
                table: "EventInfo",
                column: "ActivityId",
                principalTable: "Actvity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actvity_EventInfo_EventInfoId",
                table: "Actvity");

            migrationBuilder.DropTable(
                name: "EventInfo");

            migrationBuilder.DropTable(
                name: "Actvity");

            migrationBuilder.DropTable(
                name: "EventType");
        }
    }
}
