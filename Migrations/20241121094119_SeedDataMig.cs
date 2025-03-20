using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.Migrations
{
	/// <inheritdoc />
	public partial class SeedDataMig : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "Categories",
				columns: new[] { "Id", "Description", "Name" },
				values: new object[,]
				{
					{ 1, "First Test Category", "A" },
					{ 2, "Second Test Category", "B" },
					{ 3, "Third Test Category", "C" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValue: 2);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValue: 3);
		}
	}
}
