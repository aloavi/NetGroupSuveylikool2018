using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Disease
    {
        public int DiseaseId { get; set; }
        [Required]
        [MaxLength(120)]
        public string DiseaseName { get; set; }

        public List<Symptom> Symptoms { get; set; }
    }
}