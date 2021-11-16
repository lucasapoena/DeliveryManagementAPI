using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SetupInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportDeliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalItens = table.Column<long>(type: "bigint", nullable: false),
                    MinimalDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalDeliveryItens = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportDeliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportDeliveryItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductQty = table.Column<int>(type: "int", nullable: false),
                    ProductPrice = table.Column<int>(type: "int", nullable: false),
                    ImportDeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportDeliveryItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportDeliveryItens_ImportDeliveries_ImportDeliveryId",
                        column: x => x.ImportDeliveryId,
                        principalTable: "ImportDeliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportDeliveryItens_ImportDeliveryId",
                table: "ImportDeliveryItens",
                column: "ImportDeliveryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportDeliveryItens");

            migrationBuilder.DropTable(
                name: "ImportDeliveries");
        }
    }
}
