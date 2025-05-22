using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum StatusAtendimento
{
    DiagnosticoDeEnfermagem = 1,
    Prontuario = 2,
    PrescricaoMedica = 3,
    Evolucao = 4,
    Concluido = 5
}

public class Atendimento
{
    [Key]
    public int idAtendimento { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime dtAtendimento { get; set; }

    public int idPaciente { get; set; }
    public int? idEnfermeiro { get; set; }
    public int? idMedico { get; set; }

    [Required]
    public StatusAtendimento status { get; set; }

    [ForeignKey("idPaciente")]
    public virtual Paciente Paciente { get; set; }

    [ForeignKey("idEnfermeiro")]
    public virtual Enfermeiro Enfermeiro { get; set; }

    [ForeignKey("idMedico")]
    public virtual Medico Medico { get; set; }
}
