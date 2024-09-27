using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EvolucaoEnfermagem
{
    [Key]
    public int idEvolucaoEnfermagem { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string dataHora { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string evolucao { get; set; }

    [ForeignKey("idAtendimento")]
    public virtual Atendimento Atendimento { get; set; }
    

    [ForeignKey("idEnfermeiro")]
    public virtual Enfermeiro Enfermeiro { get; set; }


}
