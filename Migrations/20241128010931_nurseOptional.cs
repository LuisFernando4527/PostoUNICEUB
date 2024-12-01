using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class nurseOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                table: "PrescricaoEnfermagem");

            migrationBuilder.AlterColumn<int>(
                name: "idEnfermeiro",
                table: "PrescricaoEnfermagem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                table: "PrescricaoEnfermagem",
                column: "idEnfermeiro",
                principalTable: "Enfermeiro",
                principalColumn: "idEnfermeiro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                table: "PrescricaoEnfermagem");

            migrationBuilder.AlterColumn<int>(
                name: "idEnfermeiro",
                table: "PrescricaoEnfermagem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                table: "PrescricaoEnfermagem",
                column: "idEnfermeiro",
                principalTable: "Enfermeiro",
                principalColumn: "idEnfermeiro",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
