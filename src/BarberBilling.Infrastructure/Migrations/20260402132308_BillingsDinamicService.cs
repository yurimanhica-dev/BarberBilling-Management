using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BillingsDinamicService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Billings");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Billings",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientIdentifier",
                table: "Billings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BillingService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceType = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingService_Billings_BillingId",
                        column: x => x.BillingId,
                        principalTable: "Billings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingService_BillingId",
                table: "BillingService",
                column: "BillingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingService");

            migrationBuilder.DropColumn(
                name: "ClientIdentifier",
                table: "Billings");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Billings",
                newName: "Amount");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Billings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Billings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
