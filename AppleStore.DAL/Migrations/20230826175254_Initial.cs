﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppleStore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    DevicesId = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44546e06-8719-4ad8-b88a-f271ae9d6eab", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3b62472e-4f66-49fa-a20f-e7685b9565d8", 0, "3118cd99-54f9-4483-9af4-64e1c05dfa1f", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEAzlCzQPnuML15di6C7kvYasHT0T6dN1W6N4iHyGJKOXEjvlZ1u5dazZRpbdQeO44w==", null, false, "", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Apple iOS, экран 6.1\" IPS (828x1792), Apple A13 Bionic, ОЗУ 4 ГБ, память 64 ГБ, камера 12 Мп, аккумулятор 3046 мАч, 1 SIM (nano-SIM/eSIM), влагозащита IP68", "data:image/jpeg;base64,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", "Iphone 11", 1500m, 0 },
                    { 2, "Apple iOS, экран 6.1 OLED (1170x2532) 60 Гц, Apple A15 Bionic, ОЗУ 6 ГБ, память 128 ГБ, камера 12 Мп, 1 SIM (nano-SIM/eSIM), влагозащита IP68", "https://content2.onliner.by/catalog/device/header/18356816dfc5ab98f5ada5481b1d4c57.jpeg", "Iphone 14", 2680m, 0 },
                    { 3, "синхронизация с iOS, экран AMOLED, поддержка SIM-карты: нет, пульсометр, измерение кислорода в крови, ЭКГ, AOD, GPS, Bluetooth 5.0, корпус: металл (алюминий), ремешок: силикон", "data:image/jpeg;base64,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", "Apple Watch Series 7", 900m, 1 },
                    { 4, "6-го поколения, 12.9 IPS, 120 Гц (2732x2048), iOS, Apple M2, ОЗУ 8 Гб, флэш-память 128 ГБ, цвет серебристый", "https://content2.onliner.by/catalog/device/header/d6c32eeebc69cfe29e7c5d7e9ff85c33.jpeg", "Apple iPad Pro 12.9 2022", 4000m, 2 },
                    { 5, "14.2 3024 x 1964, IPS, 120 Гц, Apple M1 Pro (8 ядер), 16 ГБ, SSD 512 ГБ, видеокарта встроенная, Mac OS, цвет крышки серебристый, аккумулятор 70 Вт·ч", "https://content2.onliner.by/catalog/device/header/532ffbc1a9fa5d0ad92a3a845c8febb6.jpeg", "Apple Macbook Pro 14 M1 Pro", 6150m, 3 },
                    { 6, "5-го поколения, 10.9 IPS (2360x1640), iOS, Apple M1, ОЗУ 8 Гб, флэш-память 64 ГБ, цвет темно-серый", "https://content2.onliner.by/catalog/device/header/bf99b6a02a1e70430ee1cb0f8f8113a3.jpeg", "Apple iPad Air 2022", 2100m, 2 },
                    { 7, "15.3 2880 x 1864, IPS, 60 Гц, Apple M2, 8 ГБ, SSD 256 ГБ, видеокарта встроенная, Mac OS, цвет крышки серебристый, аккумулятор 66.5 Вт·ч", "https://content2.onliner.by/catalog/device/header/dbcc352090b0a5e348bd1b9a0327ddb5.jpeg", "Apple Macbook Air 15 M2 2023", 5300m, 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "44546e06-8719-4ad8-b88a-f271ae9d6eab", "3b62472e-4f66-49fa-a20f-e7685b9565d8" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
