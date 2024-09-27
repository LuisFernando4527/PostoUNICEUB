using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PrescricaoEnfermagem
{
    [Key]
    public int idPrescricaoEnfermagem { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string anotacao { get; set; }
}
