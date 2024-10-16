using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddedPostAnon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anonymous",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eac0f50-8963-40fa-8a8e-a2c83c2e036c", "AQAAAAIAAYagAAAAEICt2cP3LjeXfEzJENnyziCDFAWssmD5LQMSyb5rx8hnL5xkcKa7kMdAh3LJcmY3XQ==", "2a789986-cb02-4257-af28-718ce0e86cd4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anonymous",
                table: "Blogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd85a625-f381-48e6-a8b5-a547a415dd80", "AQAAAAIAAYagAAAAEEU56NNKtV9K5dZZTmqneqaoWDbcG+QCCKf6lDpzsUgiElgMuVcCQ3ERlm44LVx5TA==", "d1572af9-01f1-403c-a04a-43340056a59f" });
        }
    }
}
