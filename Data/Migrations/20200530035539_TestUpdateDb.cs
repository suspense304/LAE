using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class TestUpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInfo_Actvity_ActivityId",
                table: "EventInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_EventInfo_AspNetUsers_CreatedById",
                table: "EventInfo");

            migrationBuilder.DropIndex(
                name: "IX_EventInfo_ActivityId",
                table: "EventInfo");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "EventInfo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "EventInfo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInfo_AspNetUsers_CreatedById",
                table: "EventInfo",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInfo_AspNetUsers_CreatedById",
                table: "EventInfo");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "EventInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "EventInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EventInfoId",
                table: "Actvity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventTypeTypeId",
                table: "Actvity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventInfo_ActivityId",
                table: "EventInfo",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Actvity_EventInfoId",
                table: "Actvity",
                column: "EventInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Actvity_EventTypeTypeId",
                table: "Actvity",
                column: "EventTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actvity_EventInfo_EventInfoId",
                table: "Actvity",
                column: "EventInfoId",
                principalTable: "EventInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Actvity_EventType_EventTypeTypeId",
                table: "Actvity",
                column: "EventTypeTypeId",
                principalTable: "EventType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInfo_Actvity_ActivityId",
                table: "EventInfo",
                column: "ActivityId",
                principalTable: "Actvity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventInfo_AspNetUsers_CreatedById",
                table: "EventInfo",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
