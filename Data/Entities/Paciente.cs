using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Paciente
{

    [Key]
    public int idPaciente { get; set; }

    [Column(TypeName = "varchar(11)")]
    public string nuCPF { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string dtNascimento { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string nuCelular { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string nuDDDCelular { get; set; }
    
}
