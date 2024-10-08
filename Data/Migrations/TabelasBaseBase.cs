﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PostoCeub.Data.Entities;

namespace PostoUNICEUB.Data.Migrations
{
    [DbContext(typeof(PostoCeubDbContext)), Migration("20240927202918_Tabelas")]
    internal class TabelasBaseBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aluno", b =>
                {
                    b.Property<int>("idALuno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idALuno"));

                    b.Property<string>("curso")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("idPaciente")
                        .HasColumnType("int");

                    b.Property<string>("ra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idALuno");

                    b.HasIndex("idPaciente");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("Atendimento", b =>
                {
                    b.Property<int>("idAtendimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idAtendimento"));

                    b.Property<string>("dtAtendimento")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("idEnfermeiro")
                        .HasColumnType("int");

                    b.Property<int>("idMedico")
                        .HasColumnType("int");

                    b.Property<int>("idPaciente")
                        .HasColumnType("int");

                    b.HasKey("idAtendimento");

                    b.HasIndex("idEnfermeiro");

                    b.HasIndex("idMedico");

                    b.HasIndex("idPaciente");

                    b.ToTable("Atendimento");
                });

            modelBuilder.Entity("Colaborador", b =>
                {
                    b.Property<int>("idColaborador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idColaborador"));

                    b.Property<string>("departamento")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("idPaciente")
                        .HasColumnType("int");

                    b.Property<string>("matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idColaborador");

                    b.HasIndex("idPaciente");

                    b.ToTable("Colaborador");
                });

            modelBuilder.Entity("Diagnostico", b =>
                {
                    b.Property<int>("idDiagnostico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idDiagnostico"));

                    b.Property<string>("diagnostico")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("idDiagnostico");

                    b.ToTable("Diagnostico");
                });

            modelBuilder.Entity("DiagnosticoAtendimento", b =>
                {
                    b.Property<int>("idDiagnosticoAtendimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idDiagnosticoAtendimento"));

                    b.Property<int>("idAtendimento")
                        .HasColumnType("int");

                    b.Property<int>("idDiagnostico")
                        .HasColumnType("int");

                    b.HasKey("idDiagnosticoAtendimento");

                    b.HasIndex("idAtendimento");

                    b.HasIndex("idDiagnostico");

                    b.ToTable("DiagnosticoAtendimento");
                });

            modelBuilder.Entity("Enfermeiro", b =>
                {
                    b.Property<int>("idEnfermeiro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEnfermeiro"));

                    b.Property<string>("cre")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.HasKey("idEnfermeiro");

                    b.HasIndex("idUsuario");

                    b.ToTable("Enfermeiro");
                });

            modelBuilder.Entity("EvolucaoEnfermagem", b =>
                {
                    b.Property<int>("idEvolucaoEnfermagem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEvolucaoEnfermagem"));

                    b.Property<string>("dataHora")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("evolucao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<int>("idAtendimento")
                        .HasColumnType("int");

                    b.Property<int>("idEnfermeiro")
                        .HasColumnType("int");

                    b.HasKey("idEvolucaoEnfermagem");

                    b.HasIndex("idAtendimento");

                    b.HasIndex("idEnfermeiro");

                    b.ToTable("EvolucaoEnfermagem");
                });

            modelBuilder.Entity("Medico", b =>
                {
                    b.Property<int>("idMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idMedico"));

                    b.Property<string>("crm")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.HasKey("idMedico");

                    b.HasIndex("idUsuario");

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("Paciente", b =>
                {
                    b.Property<int>("idPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPaciente"));

                    b.Property<string>("dtNascimento")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("nuCPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<string>("nuCelular")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("nuDDDCelular")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("idPaciente");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("PrescricaoEnfermagem", b =>
                {
                    b.Property<int>("idPrescricaoEnfermagem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPrescricaoEnfermagem"));

                    b.Property<string>("anotacao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("idPrescricaoEnfermagem");

                    b.ToTable("PrescricaoEnfermagem");
                });

            modelBuilder.Entity("PrescricaoMedica", b =>
                {
                    b.Property<int>("idPrescricaoMedica")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPrescricaoMedica"));

                    b.Property<string>("horarioPrescricao")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("idAtendimento")
                        .HasColumnType("int");

                    b.Property<int>("idMedico")
                        .HasColumnType("int");

                    b.Property<string>("prescricao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("idPrescricaoMedica");

                    b.HasIndex("idAtendimento");

                    b.HasIndex("idMedico");

                    b.ToTable("PrescricaoMedica");
                });

            modelBuilder.Entity("Prontuario", b =>
                {
                    b.Property<int>("idProntuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idProntuario"));

                    b.Property<string>("conduta")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("exameFisico")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("hd")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("hda")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("hpp")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("idAtendimento")
                        .HasColumnType("int");

                    b.Property<int>("idMedico")
                        .HasColumnType("int");

                    b.Property<string>("qp")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("idProntuario");

                    b.HasIndex("idAtendimento");

                    b.HasIndex("idMedico");

                    b.ToTable("Prontuario");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("idUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUsuario"));

                    b.Property<string>("edEmail")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("nmUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("nuTelefone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("idUsuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Aluno", b =>
                {
                    b.HasOne("Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("idPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Atendimento", b =>
                {
                    b.HasOne("Enfermeiro", "Enfermeiro")
                        .WithMany()
                        .HasForeignKey("idEnfermeiro")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("idMedico")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("idPaciente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Enfermeiro");

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Colaborador", b =>
                {
                    b.HasOne("Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("idPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("DiagnosticoAtendimento", b =>
                {
                    b.HasOne("Atendimento", "Atendimento")
                        .WithMany()
                        .HasForeignKey("idAtendimento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diagnostico", "Diagnostico")
                        .WithMany()
                        .HasForeignKey("idDiagnostico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Diagnostico");
                });

            modelBuilder.Entity("Enfermeiro", b =>
                {
                    b.HasOne("Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("EvolucaoEnfermagem", b =>
                {
                    b.HasOne("Atendimento", "Atendimento")
                        .WithMany()
                        .HasForeignKey("idAtendimento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enfermeiro", "Enfermeiro")
                        .WithMany()
                        .HasForeignKey("idEnfermeiro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Enfermeiro");
                });

            modelBuilder.Entity("Medico", b =>
                {
                    b.HasOne("Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PrescricaoMedica", b =>
                {
                    b.HasOne("Atendimento", "Atendimento")
                        .WithMany()
                        .HasForeignKey("idAtendimento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("idMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Prontuario", b =>
                {
                    b.HasOne("Atendimento", "Atendimento")
                        .WithMany()
                        .HasForeignKey("idAtendimento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("idMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Medico");
                });
#pragma warning restore 612, 618
        }
    }
}