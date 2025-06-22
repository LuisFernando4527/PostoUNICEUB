using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaSenhaNaTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "senha",
                table: "Usuario",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senha",
                table: "Usuario");
        }
    }
}
