using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableClaim_newProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsExpired",
                table: "TableClaims",
                newName: "IsCanceled");

            migrationBuilder.RenameColumn(
                name: "ClaimDate",
                table: "TableClaims",
                newName: "ClaimToDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClaimFromDate",
                table: "TableClaims",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimFromDate",
                table: "TableClaims");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "TableClaims",
                newName: "IsExpired");

            migrationBuilder.RenameColumn(
                name: "ClaimToDate",
                table: "TableClaims",
                newName: "ClaimDate");
        }
    }
}
