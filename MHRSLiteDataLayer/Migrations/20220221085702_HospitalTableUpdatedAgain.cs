using Microsoft.EntityFrameworkCore.Migrations;

namespace MHRSLiteDataLayer.Migrations
{
    public partial class HospitalTableUpdatedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Hospitals",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Hospitals",
                newName: "Adress");
        }
    }
}
