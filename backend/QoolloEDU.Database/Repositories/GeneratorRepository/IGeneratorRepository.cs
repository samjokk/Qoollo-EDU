using System.Threading.Tasks;

namespace QoolloEDU.Database.Repositories.GeneratorRepository
{
    public interface IGeneratorRepository
    {
        Task GenerateDataAsync();
        Task ClearAllDataAsync();
    }
}