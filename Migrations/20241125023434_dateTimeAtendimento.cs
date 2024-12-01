using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class dateTimeAtendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "dtAtendimento",
                table: "Atendimento",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "dtAtendimento",
                table: "Atendimento",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
