using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestaurantImage",
                table: "Restaurants",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                column: "RestaurantImage",
                value: null);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                column: "RestaurantImage",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantImage",
                table: "Restaurants");
        }
    }
}
