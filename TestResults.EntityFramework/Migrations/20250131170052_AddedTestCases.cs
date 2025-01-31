using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestResults.EntityFramework.Migrations;

/// <inheritdoc />
public partial class AddedTestCases
    : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_RunTimeResults",
            table: "RunTimeResults"
        );

        migrationBuilder.DropIndex(
            name: "IX_RunTimeResults_AlgorithmName",
            table: "RunTimeResults"
        );

        migrationBuilder.DropPrimaryKey(
            name: "PK_MemoryUsageResults",
            table: "MemoryUsageResults"
        );

        migrationBuilder.DropIndex(
            name: "IX_MemoryUsageResults_AlgorithmName",
            table: "MemoryUsageResults"
        );

        migrationBuilder.DropColumn(
            name: "Id",
            table: "RunTimeResults"
        );

        migrationBuilder.DropColumn(
            name: "AlgorithmName",
            table: "RunTimeResults"
        );

        migrationBuilder.DropColumn(
            name: "IsSuccessful",
            table: "RunTimeResults"
        );

        migrationBuilder.DropColumn(
            name: "Id",
            table: "MemoryUsageResults"
        );

        migrationBuilder.DropColumn(
            name: "AlgorithmName",
            table: "MemoryUsageResults"
        );

        migrationBuilder.DropColumn(
            name: "IsSuccessful",
            table: "MemoryUsageResults"
        );

        migrationBuilder.RenameColumn(
            name: "Input",
            table: "RunTimeResults",
            newName: "TestResultId"
        );

        migrationBuilder.RenameColumn(
            name: "Input",
            table: "MemoryUsageResults",
            newName: "TestResultId"
        );

        migrationBuilder.AddPrimaryKey(
            name: "PK_RunTimeResults",
            table: "RunTimeResults",
            column: "TestResultId"
        );

        migrationBuilder.AddPrimaryKey(
            name: "PK_MemoryUsageResults",
            table: "MemoryUsageResults",
            column: "TestResultId"
        );

        migrationBuilder.CreateTable(
            name: "AlgorithmTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Type = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AlgorithmTypes", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "TestCases",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                Input = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestCases", x => x.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "Algorithms",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                TypeId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Algorithms", x => x.Id);
                table.ForeignKey(
                    name: "FK_Algorithms_AlgorithmTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "AlgorithmTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "TestResults",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                AlgorithmId = table.Column<Guid>(type: "TEXT", nullable: false),
                TestCaseId = table.Column<Guid>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestResults", x => x.Id);
                table.ForeignKey(
                    name: "FK_TestResults_Algorithms_AlgorithmId",
                    column: x => x.AlgorithmId,
                    principalTable: "Algorithms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_TestResults_TestCases_TestCaseId",
                    column: x => x.TestCaseId,
                    principalTable: "TestCases",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.InsertData(
            table: "AlgorithmTypes",
            columns: ["Id", "Type"],
            values: new object[,]
            {
                { 1, "Asymmetric" },
                { 2, "Hashing" },
                { 3, "Symmetric" }
            }
        );

        migrationBuilder.CreateIndex(
            name: "IX_Algorithms_TypeId",
            table: "Algorithms",
            column: "TypeId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_TestResults_AlgorithmId",
            table: "TestResults",
            column: "AlgorithmId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_TestResults_TestCaseId",
            table: "TestResults",
            column: "TestCaseId"
        );

        migrationBuilder.AddForeignKey(
            name: "FK_MemoryUsageResults_TestResults_TestResultId",
            table: "MemoryUsageResults",
            column: "TestResultId",
            principalTable: "TestResults",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_RunTimeResults_TestResults_TestResultId",
            table: "RunTimeResults",
            column: "TestResultId",
            principalTable: "TestResults",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_MemoryUsageResults_TestResults_TestResultId",
            table: "MemoryUsageResults"
        );

        migrationBuilder.DropForeignKey(
            name: "FK_RunTimeResults_TestResults_TestResultId",
            table: "RunTimeResults"
        );

        migrationBuilder.DropTable(
            name: "TestResults"
        );

        migrationBuilder.DropTable(
            name: "Algorithms"
        );

        migrationBuilder.DropTable(
            name: "TestCases"
        );

        migrationBuilder.DropTable(
            name: "AlgorithmTypes"
        );

        migrationBuilder.DropPrimaryKey(
            name: "PK_RunTimeResults",
            table: "RunTimeResults"
        );

        migrationBuilder.DropPrimaryKey(
            name: "PK_MemoryUsageResults",
            table: "MemoryUsageResults"
        );

        migrationBuilder.RenameColumn(
            name: "TestResultId",
            table: "RunTimeResults",
            newName: "Input"
        );

        migrationBuilder.RenameColumn(
            name: "TestResultId",
            table: "MemoryUsageResults",
            newName: "Input"
        );

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            table: "RunTimeResults",
            type: "TEXT",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
        );

        migrationBuilder.AddColumn<string>(
            name: "AlgorithmName",
            table: "RunTimeResults",
            type: "TEXT",
            nullable: false,
            defaultValue: ""
        );

        migrationBuilder.AddColumn<bool>(
            name: "IsSuccessful",
            table: "RunTimeResults",
            type: "INTEGER",
            nullable: false,
            defaultValue: false
        );

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            table: "MemoryUsageResults",
            type: "TEXT",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
        );

        migrationBuilder.AddColumn<string>(
            name: "AlgorithmName",
            table: "MemoryUsageResults",
            type: "TEXT",
            nullable: false,
            defaultValue: ""
        );

        migrationBuilder.AddColumn<bool>(
            name: "IsSuccessful",
            table: "MemoryUsageResults",
            type: "INTEGER",
            nullable: false,
            defaultValue: false
        );

        migrationBuilder.AddPrimaryKey(
            name: "PK_RunTimeResults",
            table: "RunTimeResults",
            column: "Id"
        );

        migrationBuilder.AddPrimaryKey(
            name: "PK_MemoryUsageResults",
            table: "MemoryUsageResults",
            column: "Id"
        );

        migrationBuilder.CreateIndex(
            name: "IX_RunTimeResults_AlgorithmName",
            table: "RunTimeResults",
            column: "AlgorithmName"
        );

        migrationBuilder.CreateIndex(
            name: "IX_MemoryUsageResults_AlgorithmName",
            table: "MemoryUsageResults",
            column: "AlgorithmName"
        );
    }
}
