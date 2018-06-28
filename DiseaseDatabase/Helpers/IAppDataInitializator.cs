using System.Threading.Tasks;

namespace Helpers
{
    public interface IAppDataInitializator
    {
        Task ClearDb();
        Task InitializeDbAsync();
        Task InitializeDbAsync(string[] csv);
    }
}