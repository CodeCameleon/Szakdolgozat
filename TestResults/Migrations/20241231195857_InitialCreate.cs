using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestResults.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemoryUsageResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlgorithmName = table.Column<string>(type: "TEXT", nullable: false),
                    Input = table.Column<string>(type: "TEXT", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    EncryptionMemoryUsage = table.Column<long>(type: "INTEGER", nullable: false),
                    DecryptionMemoryUsage = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryUsageResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunTimeResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlgorithmName = table.Column<string>(type: "TEXT", nullable: false),
                    Input = table.Column<string>(type: "TEXT", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimeToEncrypt = table.Column<double>(type: "REAL", nullable: false),
                    TimeToDecrypt = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunTimeResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoryUsageResults_AlgorithmName",
                table: "MemoryUsageResults",
                column: "AlgorithmName");

            migrationBuilder.CreateIndex(
                name: "IX_RunTimeResults_AlgorithmName",
                table: "RunTimeResults",
                column: "AlgorithmName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoryUsageResults");

            migrationBuilder.DropTable(
                name: "RunTimeResults");
        }
    }
}
