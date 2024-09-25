using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    
    public int idUsuario { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string nmUsuario { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string edEmail { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string nuTelefone { get; set; }

}
