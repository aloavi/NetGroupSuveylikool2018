using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using Domain;

namespace BLL.DTO
{
    public class DiseaseDTO
    {
        public int DiseaseId { get; set; }
        [Required]
        [MaxLength(120)]
        public string DiseaseName { get; set; }

        public List<SymptomDTO> Symptoms { get; set; }

        internal static DiseaseDTO CreateFromDomain(Disease disease)
        {
            if (disease == null) return null;

            return new DiseaseDTO()
            {
                DiseaseId = disease.DiseaseId,
                DiseaseName = disease.DiseaseName
            };
        }

        public static DiseaseDTO CreateFromDomainWithDiseases(Disease disease)
        {
            var diseaseDTO = CreateFromDomain(disease);
            if (diseaseDTO == null) return null;

            diseaseDTO.Symptoms = disease?.Symptoms?.Select(c => SymptomDTO.CreateFromDomain(c.Symptom)).ToList();
            return diseaseDTO;
        }

        public static Disease CreateFromDTO(DiseaseDTO dto)
        {
            if (dto == null) return null;

            return new Disease()
            {
                DiseaseId = dto.DiseaseId,
                DiseaseName = dto.DiseaseName
            };
        }
    }
}