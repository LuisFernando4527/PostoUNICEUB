using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Index(nameof(nuCPF), IsUnique = true)]
public class Paciente
{
    [Key]
    public int idPaciente { get; set; }
    
    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [Column(TypeName = "varchar(11)")]
    public string nuCPF { get; set; }

    [Column(TypeName = "varchar(50), Null;")]
    public DateTime? dtNascimento { get; set; } 


    [Required(ErrorMessage = "O campo Celular é obrigatório.")]
    [Column(TypeName = "varchar(10)")]
    public string nuCelular { get; set; }

    [Required(ErrorMessage = "O campo DDD do Celular é obrigatório.")]
    [Column(TypeName = "varchar(3)")]
    public string nuDDDCelular { get; set; }

    [Required(ErrorMessage = "O campo Nome do Paciente é obrigatório.")]
    [Column(TypeName = "varchar(100)")]
    public string nmPaciente { get; set; }

}
