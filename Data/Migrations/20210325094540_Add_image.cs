using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class Add_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Car");
        }
    }
}
