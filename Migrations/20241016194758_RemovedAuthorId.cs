using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBlog.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAuthorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd85a625-f381-48e6-a8b5-a547a415dd80", "AQAAAAIAAYagAAAAEEU56NNKtV9K5dZZTmqneqaoWDbcG+QCCKf6lDpzsUgiElgMuVcCQ3ERlm44LVx5TA==", "d1572af9-01f1-403c-a04a-43340056a59f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "AuthorId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "", "d795c21a-adf1-4e85-a9f6-07fa44a15e43", "AQAAAAIAAYagAAAAEEnBTDxrJlzYUTKQ3pJljboN5jjjuvW7iReiMFB/bid3tWRy2obj1jDO49nAVRQFcw==", "1ece55a1-1259-4cfb-be05-6bd4d148fb0a" });
        }
    }
}
