using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Colaborador
{
    [Key]
    public int idColaborador { get; set; }
    public string matricula { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string departamento { get; set; }

    [ForeignKey("idPaciente")]
    public virtual Paciente Paciente { get; set; }
        
    }
