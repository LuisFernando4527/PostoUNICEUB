using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Aluno
{
    [Key]
    public int idALuno { get; set; }

    [Required(ErrorMessage = "O campo RA é obrigatório.")]
    public string ra { get; set; }

    [Column(TypeName = "varchar(100) , Null;")]
    public string? curso { get; set; } // Não obrigatório

    [ForeignKey("idPaciente")]
    [Required(ErrorMessage = "O campo Paciente é obrigatório.")]
    public virtual Paciente Paciente { get; set; }
}
