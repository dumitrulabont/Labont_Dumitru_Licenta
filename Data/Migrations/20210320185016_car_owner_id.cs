using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class car_owner_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_CarOwnerId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarDetail_CarDetailID",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarDetail",
                table: "CarDetail");

            migrationBuilder.RenameTable(
                name: "CarDetail",
                newName: "CarDetails");

            migrationBuilder.RenameColumn(
                name: "CarOwnerId",
                table: "Car",
                newName: "CarOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CarOwnerId",
                table: "Car",
                newName: "IX_Car_CarOwnerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarDetails",
                table: "CarDetails",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_CarOwnerID",
                table: "Car",
                column: "CarOwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarDetails_CarDetailID",
                table: "Car",
                column: "CarDetailID",
                principalTable: "CarDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_CarOwnerID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarDetails_CarDetailID",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarDetails",
                table: "CarDetails");

            migrationBuilder.RenameTable(
                name: "CarDetails",
                newName: "CarDetail");

            migrationBuilder.RenameColumn(
                name: "CarOwnerID",
                table: "Car",
                newName: "CarOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CarOwnerID",
                table: "Car",
                newName: "IX_Car_CarOwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarDetail",
                table: "CarDetail",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_CarOwnerId",
                table: "Car",
                column: "CarOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarDetail_CarDetailID",
                table: "Car",
                column: "CarDetailID",
                principalTable: "CarDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
