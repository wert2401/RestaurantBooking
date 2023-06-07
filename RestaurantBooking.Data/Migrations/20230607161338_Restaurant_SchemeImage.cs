using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Restaurant_SchemeImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainPhoto",
                table: "Restaurants",
                newName: "SchemeImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchemeImage",
                table: "Restaurants",
                newName: "MainPhoto");
        }
    }
}
