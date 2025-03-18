using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Create2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "ProductHistories",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "ChangeType",
                table: "ProductHistories",
                newName: "PropertyName");

            migrationBuilder.RenameColumn(
                name: "ChangeDate",
                table: "ProductHistories",
                newName: "OldValue");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "ProductHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "ProductHistories");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "ProductHistories",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "ProductHistories",
                newName: "ChangeType");

            migrationBuilder.RenameColumn(
                name: "OldValue",
                table: "ProductHistories",
                newName: "ChangeDate");
        }
    }
}
