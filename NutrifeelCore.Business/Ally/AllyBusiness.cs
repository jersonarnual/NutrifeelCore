using NutrifeelCore.Infraestructure.DTO;
using NutrifeelCore.Infraestructure.Service;

namespace NutrifeelCore.Business.Ally
{
    public class AllyBusiness
    {
        private readonly IAllyRepository _allyRepository;
        public AllyBusiness(IAllyRepository allyRepository)
        {
            allyRepository = _allyRepository;
        }
        public async Task<bool> SaveDocumentation(AllyDTO model)
        {
            bool succed = false;
            try
            {
                return await _allyRepository.SaveDocumentation(model);
            }
            catch (Exception)
            {
                return succed;
            }
        }

    }
}
