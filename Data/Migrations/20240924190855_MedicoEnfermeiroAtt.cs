using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Data.Migrations
{
    /// <inheritdoc />
    public partial class MedicoEnfermeiroAtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enfermeiro_Usuario_UsuarioidUsuario",
                table: "Enfermeiro");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Usuario_UsuarioidUsuario",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Medico_UsuarioidUsuario",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Enfermeiro_UsuarioidUsuario",
                table: "Enfermeiro");

            migrationBuilder.DropColumn(
                name: "UsuarioidUsuario",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "UsuarioidUsuario",
                table: "Enfermeiro");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_idUsuario",
                table: "Medico",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiro_idUsuario",
                table: "Enfermeiro",
                column: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Enfermeiro_Usuario_idUsuario",
                table: "Enfermeiro",
                column: "idUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Usuario_idUsuario",
                table: "Medico",
                column: "idUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enfermeiro_Usuario_idUsuario",
                table: "Enfermeiro");

            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Usuario_idUsuario",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Medico_idUsuario",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Enfermeiro_idUsuario",
                table: "Enfermeiro");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioidUsuario",
                table: "Medico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioidUsuario",
                table: "Enfermeiro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medico_UsuarioidUsuario",
                table: "Medico",
                column: "UsuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiro_UsuarioidUsuario",
                table: "Enfermeiro",
                column: "UsuarioidUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Enfermeiro_Usuario_UsuarioidUsuario",
                table: "Enfermeiro",
                column: "UsuarioidUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Usuario_UsuarioidUsuario",
                table: "Medico",
                column: "UsuarioidUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
