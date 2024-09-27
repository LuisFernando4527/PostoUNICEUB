using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Prontuario
{
    [Key]
    public int idProntuario { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string qp { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string hda { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string hpp { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string exameFisico { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string hd { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string conduta { get; set; }

    [ForeignKey("idAtendimento")]
    public virtual Atendimento Atendimento { get; set; }

    [ForeignKey(("idMedico"))]
    public virtual Medico Medico { get; set; }

}