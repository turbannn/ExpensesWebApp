using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseWebAppDAL.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Expenses_ExpenseId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ExpenseId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryExpense",
                columns: table => new
                {
                    CategoriesListId = table.Column<int>(type: "int", nullable: false),
                    ExpensesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExpense", x => new { x.CategoriesListId, x.ExpensesId });
                    table.ForeignKey(
                        name: "FK_CategoryExpense_Categories_CategoriesListId",
                        column: x => x.CategoriesListId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryExpense_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExpense_ExpensesId",
                table: "CategoryExpense",
                column: "ExpensesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryExpense");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ExpenseId",
                table: "Categories",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Expenses_ExpenseId",
                table: "Categories",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");
        }
    }
}
