using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class ActivityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_ActivityId",
                table: "EventInfo",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInfo_Actvity_ActivityId",
                table: "EventInfo",
                column: "ActivityId",
                principalTable: "Actvity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInfo_Actvity_ActivityId",
                table: "EventInfo");

            migrationBuilder.DropIndex(
                name: "IX_EventInfo_ActivityId",
                table: "EventInfo");
        }
    }
}
