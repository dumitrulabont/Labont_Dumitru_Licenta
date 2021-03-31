using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class fk_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ReciverId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Requests",
                newName: "SenderID");

            migrationBuilder.RenameColumn(
                name: "ReciverId",
                table: "Requests",
                newName: "ReciverID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_SenderId",
                table: "Requests",
                newName: "IX_Requests_SenderID");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ReciverId",
                table: "Requests",
                newName: "IX_Requests_ReciverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ReciverID",
                table: "Requests",
                column: "ReciverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_SenderID",
                table: "Requests",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ReciverID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_SenderID",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "SenderID",
                table: "Requests",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "ReciverID",
                table: "Requests",
                newName: "ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_SenderID",
                table: "Requests",
                newName: "IX_Requests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ReciverID",
                table: "Requests",
                newName: "IX_Requests_ReciverId");

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
        }
    }
}
