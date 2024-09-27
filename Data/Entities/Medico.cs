using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



public class Medico {

    [Key]
    public int idMedico { get; set; }
    public int idUsuario { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string crm { get; set; }

    [ForeignKey("idUsuario")] 
    public virtual Usuario Usuario { get; set; }
    
}
