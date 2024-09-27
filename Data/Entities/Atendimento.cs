using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Atendimento
{
    [Key]
    public int idAtendimento { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string dtAtendimento { get; set; }

    public int idPaciente { get; set; }
    public int idEnfermeiro { get; set; }
    public int idMedico { get; set; }

    [ForeignKey("idPaciente")]
    public virtual Paciente Paciente { get; set; }

    [ForeignKey("idEnfermeiro")]
    public virtual Enfermeiro Enfermeiro { get; set; }

    [ForeignKey("idMedico")]
    public virtual Medico Medico { get; set; }
}

