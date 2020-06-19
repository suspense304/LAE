using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class PartyInfoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_PartyInfo_InfoId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Party_InfoId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Party");

            migrationBuilder.AddColumn<int>(
                name: "PartyInfoId",
                table: "Party",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_PartyInfoId",
                table: "Party",
                column: "PartyInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_PartyInfo_PartyInfoId",
                table: "Party",
                column: "PartyInfoId",
                principalTable: "PartyInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_PartyInfo_PartyInfoId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Party_PartyInfoId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PartyInfoId",
                table: "Party");

            migrationBuilder.AddColumn<int>(
                name: "InfoId",
                table: "Party",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_InfoId",
                table: "Party",
                column: "InfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_PartyInfo_InfoId",
                table: "Party",
                column: "InfoId",
                principalTable: "PartyInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
