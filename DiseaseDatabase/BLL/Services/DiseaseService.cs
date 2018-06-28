using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;
using Domain;

namespace BLL.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IAppUnitOfWork _uow;

        public DiseaseService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<DiseaseDTO>> GetTopDiseasesAsync(int take = 3)
        {
            var diseases = await _uow.Diseases.GetTopDiseasesAsync(take);
            return diseases.Select(DiseaseDTO.CreateFromDomain).ToList();
        }

        public async Task<List<DiseaseDTO>> GetAllAsync()
        {
            var diseases = await _uow.Diseases.AllAsync();
            return diseases.Select(DiseaseDTO.CreateFromDomain).OrderBy(d => d.DiseaseName).ToList();
        }

        public async Task<DiseaseDTO> GetByIdAsync(int id)
        {
            var disease = await _uow.Diseases.GetByIdWithSymptomsAsync(id);
            return DiseaseDTO.CreateFromDomainWithDiseases(disease);
        }

        public async Task<DiseaseDTO> AddAsync(DiseaseDTO dto)
        {
            var disease = DiseaseDTO.CreateFromDTO(dto);
            await _uow.Diseases.AddAsync(disease);
            if (dto.Symptoms.Any())
            {
                foreach (var dtoSymptom in dto.Symptoms)
                {
                    var symptom = await _uow.Symptoms.FindAsync(dtoSymptom.SymptomId);
                    if (symptom == null)
                    {
                        symptom = new Symptom() { SymptomName = dtoSymptom.SymptomName };
                        await _uow.Symptoms.AddAsync(symptom);
                    }
                    await _uow.DiseaseSymptoms.AddAsync(new DiseaseSymptom() { Disease = disease, Symptom = symptom });
                }
            }

            await _uow.SaveChangesAsync();
            return DiseaseDTO.CreateFromDomain(disease);
        }

        public async Task<DiseaseDTO> Update(DiseaseDTO dto)
        {
            var disease = await _uow.Diseases.FindAsync(dto.DiseaseId);
            if (disease == null) return null;

            disease.DiseaseName = dto.DiseaseName;
            disease = _uow.Diseases.Update(disease);

            // Change Symptoms
            await _uow.DiseaseSymptoms.RemoveByDiseaseAsync(disease.DiseaseId);
            if (dto.Symptoms.Any())
            {
                foreach (var dtoSymptom in dto.Symptoms)
                {
                    var symptom = await _uow.Symptoms.FindAsync(dtoSymptom.SymptomId);
                    if (symptom == null)
                    {
                        symptom = new Symptom() { SymptomName = dtoSymptom.SymptomName };
                        await _uow.Symptoms.AddAsync(symptom);
                    }
                    await _uow.DiseaseSymptoms.AddAsync(new DiseaseSymptom() { Disease = disease, Symptom = symptom });
                }
            }

            await _uow.SaveChangesAsync();

            return DiseaseDTO.CreateFromDomain(disease);
        }

        public async Task<DiseaseDTO> Remove(int id)
        {
            var diseaseDTO = await GetByIdAsync(id);
            if (diseaseDTO == null) return null;

            await _uow.DiseaseSymptoms.RemoveByDiseaseAsync(id);
            await _uow.Diseases.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return diseaseDTO;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _uow.Diseases.ExistsAsync(id);
        }
    }
}