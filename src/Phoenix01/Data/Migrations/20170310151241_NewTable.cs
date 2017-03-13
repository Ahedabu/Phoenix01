using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoenix01.Data.Migrations
{
    public partial class NewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfileInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    AreaCode = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileInfo", x => x.Id);
                });

            migrationBuilder.AddColumn<string>(
                name: "UserProfileInfoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserProfileInfoId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserProfileInfoId",
                table: "AspNetUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserProfileInfoId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileInfoId",
                table: "AspNetUsers",
                column: "UserProfileInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserProfileInfoId",
                table: "AspNetUserRoles",
                column: "UserProfileInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserProfileInfoId",
                table: "AspNetUserLogins",
                column: "UserProfileInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserProfileInfoId",
                table: "AspNetUserClaims",
                column: "UserProfileInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUserClaims",
                column: "UserProfileInfoId",
                principalTable: "UserProfileInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUserLogins",
                column: "UserProfileInfoId",
                principalTable: "UserProfileInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUserRoles",
                column: "UserProfileInfoId",
                principalTable: "UserProfileInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUsers",
                column: "UserProfileInfoId",
                principalTable: "UserProfileInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserProfileInfo_UserProfileInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserProfileInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserProfileInfoId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserProfileInfoId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_UserProfileInfoId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "UserProfileInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileInfoId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserProfileInfoId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "UserProfileInfoId",
                table: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "UserProfileInfo");
        }
    }
}
