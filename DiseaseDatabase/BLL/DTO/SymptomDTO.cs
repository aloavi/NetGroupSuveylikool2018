using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain;

namespace BLL.DTO
{
    public class SymptomDTO
    {
        public int SymptomId { get; set; }
        [Required]
        [MaxLength(120)]
        public string SymptomName { get; set; }
        public List<DiseaseDTO> Diseases { get; set; } = new List<DiseaseDTO>();

        public static SymptomDTO CreateFromDomain(Symptom s)
        {
            if (s == null) return null;

            return new SymptomDTO()
            {
                SymptomId = s.SymptomId,
                SymptomName = s.SymptomName
            };
        }

        public static SymptomDTO CreateFromDomainWithDiseases(Symptom s)
        {
            var symptom = CreateFromDomain(s);
            if (symptom == null) return null;

            symptom.Diseases = s?.Diseases?.Select(c => DiseaseDTO.CreateFromDomain(c.Disease)).ToList();
            return symptom;
        }
    }
}