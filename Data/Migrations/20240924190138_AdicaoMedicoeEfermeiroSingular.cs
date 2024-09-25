using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoMedicoeEfermeiroSingular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enfermeiros_Usuario_UsuarioidUsuario",
                table: "Enfermeiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enfermeiros",
                table: "Enfermeiros");

            migrationBuilder.RenameTable(
                name: "Enfermeiros",
                newName: "Enfermeiro");

            migrationBuilder.RenameIndex(
                name: "IX_Enfermeiros_UsuarioidUsuario",
                table: "Enfermeiro",
                newName: "IX_Enfermeiro_UsuarioidUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enfermeiro",
                table: "Enfermeiro",
                column: "idEnfermeiro");

            migrationBuilder.AddForeignKey(
                name: "FK_Enfermeiro_Usuario_UsuarioidUsuario",
                table: "Enfermeiro",
                column: "UsuarioidUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enfermeiro_Usuario_UsuarioidUsuario",
                table: "Enfermeiro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enfermeiro",
                table: "Enfermeiro");

            migrationBuilder.RenameTable(
                name: "Enfermeiro",
                newName: "Enfermeiros");

            migrationBuilder.RenameIndex(
                name: "IX_Enfermeiro_UsuarioidUsuario",
                table: "Enfermeiros",
                newName: "IX_Enfermeiros_UsuarioidUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enfermeiros",
                table: "Enfermeiros",
                column: "idEnfermeiro");

            migrationBuilder.AddForeignKey(
                name: "FK_Enfermeiros_Usuario_UsuarioidUsuario",
                table: "Enfermeiros",
                column: "UsuarioidUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
