using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Mig_0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e190f0e4-298e-450e-9304-13f51f126fa0"));

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    PermentlyDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("cc0674cc-5507-40cb-930e-b385653cbede"), 0, "f426d34b-531e-4b8b-b0cb-cee974a448e7", "rknyryn@gmail.com", true, "Kaan", "Yarayan", true, null, "RKNYRYN@GMAIL.COM", "RKNYRYN@GMAIL.COM", "AQAAAAIAAYagAAAAEMbsNcB/S2hkcdYzOMq+y0O4D9EDMtbnO10lSb23m62e5KmQM4MeEX2j5/0aIILzuQ==", "XXXXXXXXXXXXX", true, "000ec947-d4f7-4785-9a48-d46e2a100001", false, "rknyryn@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cc0674cc-5507-40cb-930e-b385653cbede"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e190f0e4-298e-450e-9304-13f51f126fa0"), 0, "a59ef8d9-e9cc-459b-8369-4667914ce1cf", "rknyryn@gmail.com", true, "Kaan", "Yarayan", true, null, "RKNYRYN@GMAIL.COM", "RKNYRYN@GMAIL.COM", "AQAAAAIAAYagAAAAEKHBh1hGaMpRsZ50VoaiTtxllCmm51pOjovHMTTbmzTy2jWLD241ahYuAaFnQ9agkg==", "XXXXXXXXXXXXX", true, "000ec947-d4f7-4785-9a48-d46e2a100001", false, "rknyryn@gmail.com" });
        }
    }
}
