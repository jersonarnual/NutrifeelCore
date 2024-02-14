using NutrifeelCore.Infraestructure.DTO;

namespace NutrifeelCore.Infraestructure.Service
{
    public interface IAllyRepository
    {
        Task<bool> SaveDocumentation(AllyDTO model);
    }
}
