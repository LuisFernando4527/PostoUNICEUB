using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class NullableMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuario_Medico_idMedico",
                table: "Prontuario");

            migrationBuilder.AlterColumn<int>(
                name: "idMedico",
                table: "Prontuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuario_Medico_idMedico",
                table: "Prontuario",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuario_Medico_idMedico",
                table: "Prontuario");

            migrationBuilder.AlterColumn<int>(
                name: "idMedico",
                table: "Prontuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuario_Medico_idMedico",
                table: "Prontuario",
                column: "idMedico",
                principalTable: "Medico",
                principalColumn: "idMedico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
