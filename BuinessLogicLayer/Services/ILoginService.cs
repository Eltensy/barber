namespace BuinessLogicLayer.Services
{
    public interface ILoginService
    {
        Task<int> Login(string email, string password);
    }
}
