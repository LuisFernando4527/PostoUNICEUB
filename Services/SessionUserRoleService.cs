using Microsoft.AspNetCore.Http;

namespace PostoUNICEUB.Services
{
	public class SessionUserRoleService : IUserRoleService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionUserRoleService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetRole()
		{
			return _httpContextAccessor.HttpContext?.Session.GetString("Perfil") ?? "";
		}

		public bool IsMedico() => GetRole().ToLower() == "medico";
		public bool IsEnfermeiro() => GetRole().ToLower() == "enfermeiro";
		public bool IsAdmin() => GetRole().ToLower() == "admin";
	}
}
