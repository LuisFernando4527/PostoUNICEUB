using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PrescricaoMedica
{
    [Key]
    public int idPrescricaoMedica { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string prescricao { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string horarioPrescricao { get; set; }

    [ForeignKey("idAtendimento")]
    public virtual Atendimento Atendimento { get; set; }
    [ForeignKey("idMedico")]
    public virtual Medico Medico { get; set; }
    
}
