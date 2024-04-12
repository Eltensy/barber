namespace BuinessLogicLayer.Services
{
    public interface ILoginService
    {
        Task<(int, int)> Login(string email, string password);
    }
}
