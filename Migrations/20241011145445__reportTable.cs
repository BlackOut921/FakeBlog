using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBlog.Migrations
{
    /// <inheritdoc />
    public partial class _reportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a69cfcf3-2132-4f13-b05c-01e616b9d84e", "AQAAAAIAAYagAAAAEFpIR9s0uZJ6tbphdwpCIRpQHkVblIV5fjmbGzOqdBbYG3udQoFHCD+3oFVg8yBZNg==", "9e632bc7-406b-4327-94a6-0433b2b0a00f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2815fee-89d8-4853-a3b4-67a0f73018e2", "AQAAAAIAAYagAAAAEK9TIKvPb2LntPIrzFfy0G2zCuo4DPcJGcDyaBv0TwTapHXI94oNjRui5LFUSAQeAg==", "dfeed760-1eec-4393-a626-b0d90a6bd62d" });
        }
    }
}
