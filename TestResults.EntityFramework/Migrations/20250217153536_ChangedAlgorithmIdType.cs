using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestResults.EntityFramework.Migrations;

/// <inheritdoc />
public partial class ChangedAlgorithmIdType
    : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "AlgorithmId",
            table: "TestResults",
            type: "INTEGER",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "TEXT"
        );

        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "Algorithms",
            type: "INTEGER",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "TEXT"
        ).Annotation("Sqlite:Autoincrement", true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<Guid>(
            name: "AlgorithmId",
            table: "TestResults",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "INTEGER"
        );

        migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "Algorithms",
            type: "TEXT",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "INTEGER"
        ).OldAnnotation("Sqlite:Autoincrement", true);
    }
}
