using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OprawaObrazowWebApi.Migrations
{
    /// <inheritdoc />
    public partial class MoveStorageLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "storage_location",
                schema: "oprawa",
                table: "frames");

            migrationBuilder.AddColumn<string>(
                name: "storage_location",
                schema: "oprawa",
                table: "frame_pieces",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "storage_location",
                schema: "oprawa",
                table: "frame_pieces");

            migrationBuilder.AddColumn<string>(
                name: "storage_location",
                schema: "oprawa",
                table: "frames",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
