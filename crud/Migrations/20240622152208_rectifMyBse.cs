using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud.Migrations
{
    /// <inheritdoc />
    public partial class rectifMyBse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "payeParJour",
                table: "Paye",
                newName: "nbSemaine");

            migrationBuilder.RenameColumn(
                name: "jourPayement",
                table: "Paye",
                newName: "mois");

            migrationBuilder.AddColumn<int>(
                name: "HeureNuit",
                table: "Paye",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NbMois",
                table: "Paye",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "heureDimanche",
                table: "Paye",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "heureTotal",
                table: "Paye",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "codeEmployee",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "heureDeTravail",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeureNuit",
                table: "Paye");

            migrationBuilder.DropColumn(
                name: "NbMois",
                table: "Paye");

            migrationBuilder.DropColumn(
                name: "heureDimanche",
                table: "Paye");

            migrationBuilder.DropColumn(
                name: "heureTotal",
                table: "Paye");

            migrationBuilder.DropColumn(
                name: "codeEmployee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "heureDeTravail",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "nbSemaine",
                table: "Paye",
                newName: "payeParJour");

            migrationBuilder.RenameColumn(
                name: "mois",
                table: "Paye",
                newName: "jourPayement");
        }
    }
}
