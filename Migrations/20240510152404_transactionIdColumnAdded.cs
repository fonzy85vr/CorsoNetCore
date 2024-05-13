using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorsoNetCore.Migrations
{
    /// <inheritdoc />
    public partial class transactionIdColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "CourseSubscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "CourseSubscriptions");
        }
    }
}
