using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoMedicoeEfermeiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enfermeiros",
                columns: table => new
                {
                    idEnfermeiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    cre = table.Column<string>(type: "varchar(20)", nullable: false),
                    UsuarioidUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiros", x => x.idEnfermeiro);
                    table.ForeignKey(
                        name: "FK_Enfermeiros_Usuario_UsuarioidUsuario",
                        column: x => x.UsuarioidUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    idMedico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    crm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioidUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.idMedico);
                    table.ForeignKey(
                        name: "FK_Medico_Usuario_UsuarioidUsuario",
                        column: x => x.UsuarioidUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enfermeiros_UsuarioidUsuario",
                table: "Enfermeiros",
                column: "UsuarioidUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_UsuarioidUsuario",
                table: "Medico",
                column: "UsuarioidUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enfermeiros");

            migrationBuilder.DropTable(
                name: "Medico");
        }
    }
}
