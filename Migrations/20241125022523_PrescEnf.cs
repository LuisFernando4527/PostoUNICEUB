using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class PrescEnf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescricaoEnfermagem_Medico_idEnfermeiro",
                table: "PrescricaoEnfermagem");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                table: "PrescricaoEnfermagem",
                column: "idEnfermeiro",
                principalTable: "Enfermeiro",
                principalColumn: "idEnfermeiro",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                table: "PrescricaoEnfermagem");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescricaoEnfermagem_Medico_idEnfermeiro",
                table: "PrescricaoEnfermagem",
                column: "idEnfermeiro",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
