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

        public static SymptomDTO CreateFromDomain(Symptom symptom)
        {
            if (symptom == null) return null;

            return new SymptomDTO()
            {
                SymptomId = symptom.SymptomId,
                SymptomName = symptom.SymptomName
            };
        }

        public static Symptom CreateFromDTO(SymptomDTO symptomDTO)
        {
            if (symptomDTO == null) return null;

            return new Symptom()
            {
                SymptomId = symptomDTO.SymptomId,
                SymptomName = symptomDTO.SymptomName
            };
        }
    }
}