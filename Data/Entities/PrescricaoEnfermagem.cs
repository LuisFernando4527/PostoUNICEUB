using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PrescricaoEnfermagem
{
    [Key]
    public int idPrescricaoEnfermagem { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string anotacao { get; set; }

    [ForeignKey("idAtendimento")]
    public virtual Atendimento Atendimento { get; set; }

    // TODO
    public int? idEnfermeiro { get; set; } // Nullable int
    [ForeignKey("idEnfermeiro")]
    public virtual Enfermeiro Enfermeiro { get; set; }
}
