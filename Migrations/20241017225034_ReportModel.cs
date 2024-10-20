using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBlog.Migrations
{
    /// <inheritdoc />
    public partial class ReportModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03ccf1ae-1019-47e4-97b6-3610e233cf71", "AQAAAAIAAYagAAAAEGUlVBvnFkoz0mvdTxJrELNWmYlDPkWV/DBeQyJJCD9vPp+bCoqAoSejsyBrDTsjVg==", "959c4a00-fd23-4c6c-96be-a40af6eceed9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eac0f50-8963-40fa-8a8e-a2c83c2e036c", "AQAAAAIAAYagAAAAEICt2cP3LjeXfEzJENnyziCDFAWssmD5LQMSyb5rx8hnL5xkcKa7kMdAh3LJcmY3XQ==", "2a789986-cb02-4257-af28-718ce0e86cd4" });
        }
    }
}
