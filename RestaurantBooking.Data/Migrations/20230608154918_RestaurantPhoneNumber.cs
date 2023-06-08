using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumbers",
                table: "Restaurants",
                newName: "PhoneNumber");

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneNumber",
                value: "+79991112233");

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhoneNumber",
                value: "+79991112233");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Restaurants",
                newName: "PhoneNumbers");

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                column: "PhoneNumbers",
                value: "[\"\\u002B79991112233\",\"\\u002B79991112233\"]");

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                column: "PhoneNumbers",
                value: "[\"\\u002B79991112233\",\"\\u002B79991112233\"]");
        }
    }
}
