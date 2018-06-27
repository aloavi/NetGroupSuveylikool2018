using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Symptom
    {
        public int SymptomId { get; set; }
        [Required]
        [MaxLength(120)]
        public string SymptomName { get; set; }
    }
}