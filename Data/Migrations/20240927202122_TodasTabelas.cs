using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Data.Migrations
{
    /// <inheritdoc />
    public partial class TodasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "crm",
                table: "Medico",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Diagnostico",
                columns: table => new
                {
                    idDiagnostico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diagnostico = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostico", x => x.idDiagnostico);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    idPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nuCPF = table.Column<string>(type: "varchar(11)", nullable: false),
                    dtNascimento = table.Column<string>(type: "varchar(50)", nullable: false),
                    nuCelular = table.Column<string>(type: "varchar(50)", nullable: false),
                    nuDDDCelular = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.idPaciente);
                });

            migrationBuilder.CreateTable(
                name: "PrescricaoEnfermagem",
                columns: table => new
                {
                    idPrescricaoEnfermagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anotacao = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricaoEnfermagem", x => x.idPrescricaoEnfermagem);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    idALuno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    curso = table.Column<string>(type: "varchar(100)", nullable: false),
                    idPaciente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.idALuno);
                    table.ForeignKey(
                        name: "FK_Aluno_Paciente_idPaciente",
                        column: x => x.idPaciente,
                        principalTable: "Paciente",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    idAtendimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtAtendimento = table.Column<string>(type: "varchar(20)", nullable: false),
                    idPaciente = table.Column<int>(type: "int", nullable: false),
                    idEnfermeiro = table.Column<int>(type: "int", nullable: false),
                    idMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.idAtendimento);
                    table.ForeignKey(
                        name: "FK_Atendimento_Enfermeiro_idEnfermeiro",
                        column: x => x.idEnfermeiro,
                        principalTable: "Enfermeiro",
                        principalColumn: "idEnfermeiro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimento_Medico_idMedico",
                        column: x => x.idMedico,
                        principalTable: "Medico",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimento_Paciente_idPaciente",
                        column: x => x.idPaciente,
                        principalTable: "Paciente",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    idColaborador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departamento = table.Column<string>(type: "varchar(100)", nullable: false),
                    idPaciente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.idColaborador);
                    table.ForeignKey(
                        name: "FK_Colaborador_Paciente_idPaciente",
                        column: x => x.idPaciente,
                        principalTable: "Paciente",
                        principalColumn: "idPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticoAtendimento",
                columns: table => new
                {
                    idDiagnosticoAtendimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDiagnostico = table.Column<int>(type: "int", nullable: false),
                    idAtendimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticoAtendimento", x => x.idDiagnosticoAtendimento);
                    table.ForeignKey(
                        name: "FK_DiagnosticoAtendimento_Atendimento_idAtendimento",
                        column: x => x.idAtendimento,
                        principalTable: "Atendimento",
                        principalColumn: "idAtendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticoAtendimento_Diagnostico_idDiagnostico",
                        column: x => x.idDiagnostico,
                        principalTable: "Diagnostico",
                        principalColumn: "idDiagnostico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvolucaoEnfermagem",
                columns: table => new
                {
                    idEvolucaoEnfermagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataHora = table.Column<string>(type: "varchar(50)", nullable: false),
                    evolucao = table.Column<string>(type: "varchar(500)", nullable: false),
                    idAtendimento = table.Column<int>(type: "int", nullable: false),
                    idEnfermeiro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolucaoEnfermagem", x => x.idEvolucaoEnfermagem);
                    table.ForeignKey(
                        name: "FK_EvolucaoEnfermagem_Atendimento_idAtendimento",
                        column: x => x.idAtendimento,
                        principalTable: "Atendimento",
                        principalColumn: "idAtendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvolucaoEnfermagem_Enfermeiro_idEnfermeiro",
                        column: x => x.idEnfermeiro,
                        principalTable: "Enfermeiro",
                        principalColumn: "idEnfermeiro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescricaoMedica",
                columns: table => new
                {
                    idPrescricaoMedica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prescricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    horarioPrescricao = table.Column<string>(type: "varchar(50)", nullable: false),
                    idAtendimento = table.Column<int>(type: "int", nullable: false),
                    idMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricaoMedica", x => x.idPrescricaoMedica);
                    table.ForeignKey(
                        name: "FK_PrescricaoMedica_Atendimento_idAtendimento",
                        column: x => x.idAtendimento,
                        principalTable: "Atendimento",
                        principalColumn: "idAtendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescricaoMedica_Medico_idMedico",
                        column: x => x.idMedico,
                        principalTable: "Medico",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prontuario",
                columns: table => new
                {
                    idProntuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    qp = table.Column<string>(type: "varchar(100)", nullable: false),
                    hda = table.Column<string>(type: "varchar(100)", nullable: false),
                    hpp = table.Column<string>(type: "varchar(100)", nullable: false),
                    exameFisico = table.Column<string>(type: "varchar(500)", nullable: false),
                    hd = table.Column<string>(type: "varchar(100)", nullable: false),
                    conduta = table.Column<string>(type: "varchar(500)", nullable: false),
                    idAtendimento = table.Column<int>(type: "int", nullable: false),
                    idMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuario", x => x.idProntuario);
                    table.ForeignKey(
                        name: "FK_Prontuario_Atendimento_idAtendimento",
                        column: x => x.idAtendimento,
                        principalTable: "Atendimento",
                        principalColumn: "idAtendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prontuario_Medico_idMedico",
                        column: x => x.idMedico,
                        principalTable: "Medico",
                        principalColumn: "idMedico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_idPaciente",
                table: "Aluno",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_idEnfermeiro",
                table: "Atendimento",
                column: "idEnfermeiro");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_idMedico",
                table: "Atendimento",
                column: "idMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_idPaciente",
                table: "Atendimento",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_idPaciente",
                table: "Colaborador",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoAtendimento_idAtendimento",
                table: "DiagnosticoAtendimento",
                column: "idAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoAtendimento_idDiagnostico",
                table: "DiagnosticoAtendimento",
                column: "idDiagnostico");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucaoEnfermagem_idAtendimento",
                table: "EvolucaoEnfermagem",
                column: "idAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucaoEnfermagem_idEnfermeiro",
                table: "EvolucaoEnfermagem",
                column: "idEnfermeiro");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricaoMedica_idAtendimento",
                table: "PrescricaoMedica",
                column: "idAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricaoMedica_idMedico",
                table: "PrescricaoMedica",
                column: "idMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_idAtendimento",
                table: "Prontuario",
                column: "idAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_idMedico",
                table: "Prontuario",
                column: "idMedico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.DropTable(
                name: "DiagnosticoAtendimento");

            migrationBuilder.DropTable(
                name: "EvolucaoEnfermagem");

            migrationBuilder.DropTable(
                name: "PrescricaoEnfermagem");

            migrationBuilder.DropTable(
                name: "PrescricaoMedica");

            migrationBuilder.DropTable(
                name: "Prontuario");

            migrationBuilder.DropTable(
                name: "Diagnostico");

            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.AlterColumn<string>(
                name: "crm",
                table: "Medico",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
