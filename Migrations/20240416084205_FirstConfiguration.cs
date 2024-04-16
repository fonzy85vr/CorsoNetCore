using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorsoNetCore.Migrations
{
    /// <inheritdoc />
    public partial class FirstConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    CurrentPrice_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentPrice_Amount = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    FullPrice_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    FullPrice_Amount = table.Column<decimal>(type: "NUMERIC", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
