using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Aluno
{
    [Key]
    public int idALuno { get; set; }
    public string ra { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string curso { get; set; }

    [ForeignKey("idPaciente")]
    public virtual Paciente Paciente{ get; set; }   
    

}
