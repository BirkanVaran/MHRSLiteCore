using Microsoft.EntityFrameworkCore.Migrations;

namespace MHRSLiteDataLayer.Migrations
{
    public partial class HospitalTableColumnNameUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "Hospitals",
                newName: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Hospitals",
                newName: "Longitute");
        }
    }
}
