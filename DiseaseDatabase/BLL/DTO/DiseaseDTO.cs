using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain;

namespace BLL.DTO
{
    public class DiseaseDTO
    {
        public int DiseaseId { get; set; }
        [Required]
        [MaxLength(120)]
        public string DiseaseName { get; set; }
        public List<SymptomDTO> Symptoms { get; set; } = new List<SymptomDTO>();

        internal static DiseaseDTO CreateFromDomain(Disease d)
        {
            if (d == null) return null;

            return new DiseaseDTO()
            {
                DiseaseId = d.DiseaseId,
                DiseaseName = d.DiseaseName
            };
        }

        public static DiseaseDTO CreateFromDomainWithDiseases(Disease d)
        {
            var disease = CreateFromDomain(d);
            if (disease == null) return null;

            disease.Symptoms = d?.Symptoms?.Select(c => SymptomDTO.CreateFromDomain(c.Symptom)).ToList();
            return disease;
        }
    }
}