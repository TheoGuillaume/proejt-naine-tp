using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud.Migrations
{
    /// <inheritdoc />
    public partial class rectifBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paye_Employees_EmployeeId",
                table: "Paye");

            migrationBuilder.DropForeignKey(
                name: "FK_Paye_Paye_PayeId",
                table: "Paye");

            migrationBuilder.DropIndex(
                name: "IX_Paye_PayeId",
                table: "Paye");

            migrationBuilder.DropColumn(
                name: "PayeId",
                table: "Paye");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Paye",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paye_Employees_EmployeeId",
                table: "Paye",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paye_Employees_EmployeeId",
                table: "Paye");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Paye",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PayeId",
                table: "Paye",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paye_PayeId",
                table: "Paye",
                column: "PayeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paye_Employees_EmployeeId",
                table: "Paye",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paye_Paye_PayeId",
                table: "Paye",
                column: "PayeId",
                principalTable: "Paye",
                principalColumn: "Id");
        }
    }
}
