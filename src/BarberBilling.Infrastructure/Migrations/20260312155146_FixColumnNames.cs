using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Billings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Billings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "serviceName",
                table: "Billings",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "paymentMethod",
                table: "Billings",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Billings",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Billings",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "clientName",
                table: "Billings",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "barberName",
                table: "Billings",
                newName: "BarberName");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Billings",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Billings",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Billings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "Billings",
                newName: "serviceName");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Billings",
                newName: "paymentMethod");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Billings",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Billings",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Billings",
                newName: "clientName");

            migrationBuilder.RenameColumn(
                name: "BarberName",
                table: "Billings",
                newName: "barberName");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Billings",
                newName: "amount");
        }
    }
}
