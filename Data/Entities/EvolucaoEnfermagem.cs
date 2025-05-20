using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EvolucaoEnfermagem
{
    [Key]
    public int idEvolucaoEnfermagem { get; set; }

    [Column(TypeName = "varchar(50)")]
    public DateTime dataHora { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string evolucao { get; set; }

    [ForeignKey("idAtendimento")]
    public virtual Atendimento Atendimento { get; set; }


    // TODO
    public int? idEnfermeiro { get; set; } // Nullable int
    [ForeignKey("idEnfermeiro")]
    public virtual Enfermeiro Enfermeiro { get; set; }


}
