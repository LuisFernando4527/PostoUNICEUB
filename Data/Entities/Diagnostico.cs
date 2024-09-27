using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Diagnostico
{
    [Key]
    public int idDiagnostico { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string diagnostico { get; set; }
}
