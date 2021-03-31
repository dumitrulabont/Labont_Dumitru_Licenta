using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class RequestCarFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Contract_ContractID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_AspNetUsers_CarOwnerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_AspNetUsers_CarReciverId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Request_RequestID",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_ReciverId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_SenderId",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Request_SenderId",
                table: "Requests",
                newName: "IX_Requests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_ReciverId",
                table: "Requests",
                newName: "IX_Requests_ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_RequestID",
                table: "Contracts",
                newName: "IX_Contracts_RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CarReciverId",
                table: "Contracts",
                newName: "IX_Contracts_CarReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CarOwnerId",
                table: "Contracts",
                newName: "IX_Contracts_CarOwnerId");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CarId",
                table: "Requests",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Contracts_ContractID",
                table: "Car",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_CarOwnerId",
                table: "Contracts",
                column: "CarOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_CarReciverId",
                table: "Contracts",
                column: "CarReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Requests_RequestID",
                table: "Contracts",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ReciverId",
                table: "Requests",
                column: "ReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Car_CarId",
                table: "Requests",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Contracts_ContractID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_CarOwnerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_CarReciverId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Requests_RequestID",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ReciverId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Car_CarId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CarId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_SenderId",
                table: "Request",
                newName: "IX_Request_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ReciverId",
                table: "Request",
                newName: "IX_Request_ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_RequestID",
                table: "Contract",
                newName: "IX_Contract_RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CarReciverId",
                table: "Contract",
                newName: "IX_Contract_CarReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CarOwnerId",
                table: "Contract",
                newName: "IX_Contract_CarOwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Contract_ContractID",
                table: "Car",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_AspNetUsers_CarOwnerId",
                table: "Contract",
                column: "CarOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_AspNetUsers_CarReciverId",
                table: "Contract",
                column: "CarReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Request_RequestID",
                table: "Contract",
                column: "RequestID",
                principalTable: "Request",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_ReciverId",
                table: "Request",
                column: "ReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_SenderId",
                table: "Request",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
