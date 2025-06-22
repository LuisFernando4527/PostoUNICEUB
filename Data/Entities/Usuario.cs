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

    // ✅ INÍCIO: Alteração para adicionar o campo de senha
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [Column(TypeName = "varchar(200)")] // Tamanho maior para senhas "hasheadas" no futuro
    public string senha { get; set; }
    // ✅ FIM: Alteração
}