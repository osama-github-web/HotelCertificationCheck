using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelCertificationCheck.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class BecauseIdAddedToHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BecauseId",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BecauseId",
                table: "Hotels");
        }
    }
}
