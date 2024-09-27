using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DiagnosticoAtendimento
{

    [Key]
    public int idDiagnosticoAtendimento { get; set; }

    [ForeignKey("idDiagnostico")]
    public virtual Diagnostico Diagnostico {  get; set; }

    [ForeignKey("idAtendimento")]
    public virtual Atendimento Atendimento { get; set; }
    public int idAtendimento { get; set; }
}