using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




public class Enfermeiro
{

    [Key]
    public int idEnfermeiro { get; set; }

    public int idUsuario { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string cre { get; set; }

    [ForeignKey("idUsuario")]
    public virtual Usuario Usuario { get; set; }
}
