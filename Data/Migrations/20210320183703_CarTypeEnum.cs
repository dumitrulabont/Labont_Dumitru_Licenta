using Microsoft.EntityFrameworkCore.Migrations;

namespace Labont_Dumitru_Licenta.Data.Migrations
{
    public partial class CarTypeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarType",
                table: "CarDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarType",
                table: "CarDetail");
        }
    }
}
