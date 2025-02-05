using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestResults.EntityFramework.Migrations;

/// <inheritdoc />
public partial class AddedTestCaseSize
    : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "Size",
            table: "TestCases",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Size",
            table: "TestCases"
        );
    }
}
