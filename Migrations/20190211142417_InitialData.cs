using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHotel.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Name", "RegisterDate" },
                values: new object[,]
                {
                    { 1, "Alper Ebicoglu", new DateTime(2019, 2, 1, 17, 24, 17, 42, DateTimeKind.Local).AddTicks(4605) },
                    { 2, "George Michael", new DateTime(2019, 2, 6, 17, 24, 17, 45, DateTimeKind.Local).AddTicks(7666) },
                    { 3, "Daft Punk", new DateTime(2019, 2, 10, 17, 24, 17, 45, DateTimeKind.Local).AddTicks(7851) }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AllowedSmoking", "Name", "Number", "Status" },
                values: new object[,]
                {
                    { 1, false, "yellow-room", 101, 1 },
                    { 2, false, "blue-room", 102, 1 },
                    { 3, false, "white-room", 103, 0 },
                    { 4, false, "black-room", 104, 0 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckinDate", "CheckoutDate", "GuestId", "RoomId" },
                values: new object[] { 1, new DateTime(2019, 2, 11, 17, 24, 17, 46, DateTimeKind.Local).AddTicks(3759), new DateTime(2019, 2, 21, 17, 24, 17, 46, DateTimeKind.Local).AddTicks(3849), 1, 3 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckinDate", "CheckoutDate", "GuestId", "RoomId" },
                values: new object[] { 2, new DateTime(2019, 2, 11, 17, 24, 17, 46, DateTimeKind.Local).AddTicks(9405), new DateTime(2019, 2, 21, 17, 24, 17, 46, DateTimeKind.Local).AddTicks(9540), 2, 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
