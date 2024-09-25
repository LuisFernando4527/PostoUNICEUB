using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Data.Migrations
{
    /// <inheritdoc />
    public partial class MedicoEnfermeiroAttTelefone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nmTelefone",
                table: "Usuario",
                newName: "nuTelefone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nuTelefone",
                table: "Usuario",
                newName: "nmTelefone");
        }
    }
}
