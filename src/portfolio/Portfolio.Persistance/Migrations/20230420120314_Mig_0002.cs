using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Mig_0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e190f0e4-298e-450e-9304-13f51f126fa0"), 0, "a59ef8d9-e9cc-459b-8369-4667914ce1cf", "rknyryn@gmail.com", true, "Kaan", "Yarayan", true, null, "RKNYRYN@GMAIL.COM", "RKNYRYN@GMAIL.COM", "AQAAAAIAAYagAAAAEKHBh1hGaMpRsZ50VoaiTtxllCmm51pOjovHMTTbmzTy2jWLD241ahYuAaFnQ9agkg==", "XXXXXXXXXXXXX", true, "000ec947-d4f7-4785-9a48-d46e2a100001", false, "rknyryn@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e190f0e4-298e-450e-9304-13f51f126fa0"));
        }
    }
}
