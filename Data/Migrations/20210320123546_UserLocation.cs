using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class UserLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLocation_AspNetUsers_Labont_Dumitru_LicentaUserID",
                table: "UserLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLocation",
                table: "UserLocation");

            migrationBuilder.RenameTable(
                name: "UserLocation",
                newName: "UserLocations");

            migrationBuilder.RenameIndex(
                name: "IX_UserLocation_Labont_Dumitru_LicentaUserID",
                table: "UserLocations",
                newName: "IX_UserLocations_Labont_Dumitru_LicentaUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLocations",
                table: "UserLocations",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_AspNetUsers_Labont_Dumitru_LicentaUserID",
                table: "UserLocations",
                column: "Labont_Dumitru_LicentaUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_AspNetUsers_Labont_Dumitru_LicentaUserID",
                table: "UserLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLocations",
                table: "UserLocations");

            migrationBuilder.RenameTable(
                name: "UserLocations",
                newName: "UserLocation");

            migrationBuilder.RenameIndex(
                name: "IX_UserLocations_Labont_Dumitru_LicentaUserID",
                table: "UserLocation",
                newName: "IX_UserLocation_Labont_Dumitru_LicentaUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLocation",
                table: "UserLocation",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocation_AspNetUsers_Labont_Dumitru_LicentaUserID",
                table: "UserLocation",
                column: "Labont_Dumitru_LicentaUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
