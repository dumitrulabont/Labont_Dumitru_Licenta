using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class makre_contractID_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Contract_ContractID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_ContractID",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "Car",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ContractID",
                table: "Car",
                column: "ContractID",
                unique: true,
                filter: "[ContractID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Contract_ContractID",
                table: "Car",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Contract_ContractID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_ContractID",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "ContractID",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_ContractID",
                table: "Car",
                column: "ContractID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Contract_ContractID",
                table: "Car",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
