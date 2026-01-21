using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoilerPlateNetCore10.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Complement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Number = table.Column<int>(type: "int", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CNPJ_Number = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Complement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Number = table.Column<int>(type: "int", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CPF_Number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Since = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    Admission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Resignation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Enterprises_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Enterprises_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_EmployerId",
                table: "People",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_People_SupplierId",
                table: "People",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Enterprises");
        }
    }
}
