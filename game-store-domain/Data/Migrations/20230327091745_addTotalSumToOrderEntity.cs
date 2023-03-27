using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace game_store_domain.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTotalSumToOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalSum",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSum",
                table: "Orders");
        }
    }
}
