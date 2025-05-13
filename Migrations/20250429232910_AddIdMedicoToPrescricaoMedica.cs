using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class AddIdMedicoToPrescricaoMedica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescricaoMedica_Medico_idMedico",
                table: "PrescricaoMedica");

            migrationBuilder.AlterColumn<int>(
                name: "idMedico",
                table: "PrescricaoMedica",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescricaoMedica_Medico_idMedico",
                table: "PrescricaoMedica",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescricaoMedica_Medico_idMedico",
                table: "PrescricaoMedica");

            migrationBuilder.AlterColumn<int>(
                name: "idMedico",
                table: "PrescricaoMedica",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescricaoMedica_Medico_idMedico",
                table: "PrescricaoMedica",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
