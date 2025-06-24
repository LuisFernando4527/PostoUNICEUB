using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostoUNICEUB.Migrations
{
    /// <inheritdoc />
    public partial class SchemaInicialDefinitivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    dtNascimento = table.Column<string>(type: "varchar(50)", nullable: true),
                    nuCelular = table.Column<string>(type: "varchar(10)", nullable: false),
                    nuDDDCelular = table.Column<string>(type: "varchar(3)", nullable: false),
                    nmPaciente = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.idPaciente);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmUsuario = table.Column<string>(type: "varchar(100)", nullable: false),
                    edEmail = table.Column<string>(type: "varchar(100)", nullable: false),
                    nuTelefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    senha = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    idALuno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    curso = table.Column<string>(type: "varchar(100)", nullable: true),
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
                name: "Enfermeiro",
                columns: table => new
                {
                    idEnfermeiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    cre = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeiro", x => x.idEnfermeiro);
                    table.ForeignKey(
                        name: "FK_Enfermeiro_Usuario_idUsuario",
                        column: x => x.idUsuario,
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
                    crm = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.idMedico);
                    table.ForeignKey(
                        name: "FK_Medico_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    idAtendimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtAtendimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idPaciente = table.Column<int>(type: "int", nullable: false),
                    idEnfermeiro = table.Column<int>(type: "int", nullable: true),
                    idMedico = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.idAtendimento);
                    table.ForeignKey(
                        name: "FK_Atendimento_Enfermeiro_idEnfermeiro",
                        column: x => x.idEnfermeiro,
                        principalTable: "Enfermeiro",
                        principalColumn: "idEnfermeiro");
                    table.ForeignKey(
                        name: "FK_Atendimento_Medico_idMedico",
                        column: x => x.idMedico,
                        principalTable: "Medico",
                        principalColumn: "idMedico");
                    table.ForeignKey(
                        name: "FK_Atendimento_Paciente_idPaciente",
                        column: x => x.idPaciente,
                        principalTable: "Paciente",
                        principalColumn: "idPaciente");
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
                    idEnfermeiro = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "idEnfermeiro");
                });

            migrationBuilder.CreateTable(
                name: "PrescricaoEnfermagem",
                columns: table => new
                {
                    idPrescricaoEnfermagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anotacao = table.Column<string>(type: "varchar(500)", nullable: false),
                    idAtendimento = table.Column<int>(type: "int", nullable: false),
                    idEnfermeiro = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricaoEnfermagem", x => x.idPrescricaoEnfermagem);
                    table.ForeignKey(
                        name: "FK_PrescricaoEnfermagem_Atendimento_idAtendimento",
                        column: x => x.idAtendimento,
                        principalTable: "Atendimento",
                        principalColumn: "idAtendimento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescricaoEnfermagem_Enfermeiro_idEnfermeiro",
                        column: x => x.idEnfermeiro,
                        principalTable: "Enfermeiro",
                        principalColumn: "idEnfermeiro");
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
                    idMedico = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "idMedico");
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
                    idMedico = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "idMedico");
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
                name: "IX_Enfermeiro_idUsuario",
                table: "Enfermeiro",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucaoEnfermagem_idAtendimento",
                table: "EvolucaoEnfermagem",
                column: "idAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucaoEnfermagem_idEnfermeiro",
                table: "EvolucaoEnfermagem",
                column: "idEnfermeiro");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_idUsuario",
                table: "Medico",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_nuCPF",
                table: "Paciente",
                column: "nuCPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrescricaoEnfermagem_idAtendimento",
                table: "PrescricaoEnfermagem",
                column: "idAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricaoEnfermagem_idEnfermeiro",
                table: "PrescricaoEnfermagem",
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
                name: "Enfermeiro");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
