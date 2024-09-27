using Microsoft.EntityFrameworkCore;

namespace PostoUNICEUB.Data.Entities
{
    public class PostoCeubDbContext : DbContext
    {
        public PostoCeubDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Colaborador> Colaborador { get; set; }
        public DbSet<Diagnostico> Diagnostico { get; set; }
        public DbSet<DiagnosticoAtendimento> DiagnosticoAtendimento { get; set; }
        public DbSet<Enfermeiro> Enfermeiro { get; set; }
        public DbSet<EvolucaoEnfermagem> EvolucaoEnfermagem { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<PrescricaoEnfermagem> PrescricaoEnfermagem { get; set; }
        public DbSet<PrescricaoMedica> PrescricaoMedica { get; set; }
        public DbSet<Prontuario> Prontuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Paciente)
                .WithMany()
                .HasForeignKey(a => a.idPaciente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Enfermeiro)
                .WithMany()
                .HasForeignKey(a => a.idEnfermeiro)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Atendimento>()
                .HasOne(a => a.Medico)
                .WithMany()
                .HasForeignKey(a => a.idMedico)
                .OnDelete(DeleteBehavior.NoAction);

            // Adicione outras configurações de relacionamento conforme necessário
        }
    }
}
