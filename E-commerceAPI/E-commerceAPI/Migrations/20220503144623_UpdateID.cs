using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerceAPI.Migrations
{
    public partial class UpdateID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreDTO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Store",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Phone",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Branche",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Address",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Store",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Phone",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Category",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Branche",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Address",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "StoreDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDTO", x => x.Id);
                });
        }
    }
}
