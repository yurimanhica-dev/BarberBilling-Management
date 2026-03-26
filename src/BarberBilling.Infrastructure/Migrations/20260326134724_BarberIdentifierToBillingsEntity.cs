using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBilling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BarberIdentifierToBillingsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarberName",
                table: "Billings");

            migrationBuilder.AddColumn<Guid>(
                name: "BarberIdentifier",
                table: "Billings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarberIdentifier",
                table: "Billings");

            migrationBuilder.AddColumn<string>(
                name: "BarberName",
                table: "Billings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
