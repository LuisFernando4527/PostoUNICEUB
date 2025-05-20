namespace PostoUNICEUB.Services
{
    public class MockUserRoleService : IUserRoleService
    {
        private readonly string _role;

        public MockUserRoleService(string role)
        {
            _role = role.ToLower();
        }

        public string GetRole() => _role;
        public bool IsMedico() => _role == "medico";
        public bool IsEnfermeiro() => _role == "enfermeiro";
        public bool IsAdmin() => _role == "admin";
    }
}
