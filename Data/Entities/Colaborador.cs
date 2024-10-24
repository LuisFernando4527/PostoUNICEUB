using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Colaborador
{
    [Key]
    public int idColaborador { get; set; }

    [Required(ErrorMessage = "O campo Matrícula é obrigatório.")]
    public string matricula { get; set; }

    [Column(TypeName = "varchar(100) , Null;")]
    public string departamento { get; set; } // Não obrigatório

    [ForeignKey("idPaciente")]
    [Required(ErrorMessage = "O campo Paciente é obrigatório.")]
    public virtual Paciente Paciente { get; set; }
}
