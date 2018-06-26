using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Disease
    {
        public int DiseaseId { get; set; }
        [Required]
        [MaxLength(120)]
        public string DiseaseName { get; set; }

        public List<DiseaseSymptom> Symptoms { get; set; }
    }
}