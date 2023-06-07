using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantTablesCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "TablesCount",
                table: "Restaurants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                column: "TablesCount",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                column: "TablesCount",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TablesCount",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Tables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PositionX",
                table: "Tables",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PositionY",
                table: "Tables",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "PositionX", "PositionY" },
                values: new object[] { 4, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "PositionX", "PositionY" },
                values: new object[] { 3, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "PositionX", "PositionY" },
                values: new object[] { 3, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Capacity", "PositionX", "PositionY" },
                values: new object[] { 14, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Capacity", "PositionX", "PositionY" },
                values: new object[] { 13, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Capacity", "PositionX", "PositionY" },
                values: new object[] { 13, 0.0, 0.0 });
        }
    }
}
