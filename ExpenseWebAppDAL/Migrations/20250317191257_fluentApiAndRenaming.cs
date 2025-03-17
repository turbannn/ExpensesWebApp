using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseWebAppDAL.Migrations
{
    /// <inheritdoc />
    public partial class fluentApiAndRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE Expenses CHANGE COLUMN Value expense_value DOUBLE;");

            migrationBuilder.Sql(@"ALTER TABLE Expenses CHANGE COLUMN Description expense_description LONGTEXT;");

            migrationBuilder.Sql(@"ALTER TABLE Expenses CHANGE COLUMN CreationDate expense_creationDate DATETIME;");

            migrationBuilder.Sql(@"ALTER TABLE Expenses CHANGE COLUMN Categories expense_categories LONGTEXT;");

            migrationBuilder.Sql(@"ALTER TABLE Expenses CHANGE COLUMN Id expense_id INT;");

            migrationBuilder.Sql(@"ALTER TABLE `Expenses`
                                    MODIFY COLUMN `expense_creationDate` DATETIME(6) NULL DEFAULT CURRENT_TIMESTAMP;");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "varchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "expense_value",
                table: "Expenses",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "expense_description",
                table: "Expenses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "expense_creationDate",
                table: "Expenses",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "expense_categories",
                table: "Expenses",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "expense_id",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Expenses",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldMaxLength: 35)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
