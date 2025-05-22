namespace PostoUNICEUB.Models
{
	public class AtendimentoCardModel
	{
		public Atendimento Atendimento { get; set; }
		public string StatusNome { get; set; }
		public string StatusClass { get; set; }
		public string StatusBtn { get; set; }
		public bool MostrarPreencher { get; set; }
		public string? LinkPreencher { get; set; }
		public bool PodeEditar { get; set; }
	}
}
