using NutrifeelCore.Infraestructure.DTO;
using NutrifeelCore.Infraestructure.Service;

namespace NutrifeelCore.Infraestructure.Repository
{
    public class AllyRepository : IAllyRepository
    {

        public async Task<bool> SaveDocumentation(AllyDTO model)
        {

            return true;
        }
    }
}
