namespace PostoUNICEUB.Services
{
    public interface IUserRoleService
    {
        string GetRole();
        bool IsMedico();
        bool IsEnfermeiro();
        bool IsAdmin();
    }
}
