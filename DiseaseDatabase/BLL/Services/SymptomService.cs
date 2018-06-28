using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;

namespace BLL.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly IAppUnitOfWork _uow;

        public SymptomService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> SymptomCountAsync()
        {
            return await _uow.Symptoms.CountAsync();
        }

        public async Task<List<SymptomDTO>> GetTopSymptomsAsync(int take = 3)
        {
            var syptom = await _uow.Symptoms.GetTopSymptomsAsync(take);
            return syptom.Select(SymptomDTO.CreateFromDomain).ToList();
        }

        public async Task<List<SymptomDTO>> GetAllAsync()
        {
            var symptoms = await _uow.Symptoms.AllAsync();
            return symptoms.Select(SymptomDTO.CreateFromDomain).OrderBy(s => s.SymptomName).ToList();
        }

        public async Task<SymptomDTO> GetByIdAsync(int id)
        {
            var symptom = await _uow.Symptoms.FindAsync(id);
            return SymptomDTO.CreateFromDomain(symptom);
        }

        public async Task<SymptomDTO> AddAsync(SymptomDTO dto)
        {
            var symptom = SymptomDTO.CreateFromDTO(dto);

            await _uow.Symptoms.AddAsync(symptom);
            await _uow.SaveChangesAsync();

            return SymptomDTO.CreateFromDomain(symptom);
        }

        public async Task<SymptomDTO> Update(SymptomDTO dto)
        {
            var symptom = await _uow.Symptoms.FindAsync(dto.SymptomId);
            if (symptom == null) return null;

            symptom.SymptomName = dto.SymptomName;

            symptom = _uow.Symptoms.Update(symptom);
            await _uow.SaveChangesAsync();

            return SymptomDTO.CreateFromDomain(symptom);
        }

        public async Task<SymptomDTO> Remove(int id)
        {
            var symptomDTO = await GetByIdAsync(id);
            if (symptomDTO == null) return null;

            await _uow.Symptoms.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return symptomDTO;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _uow.Symptoms.ExistsAsync(id);
        }
    }
}