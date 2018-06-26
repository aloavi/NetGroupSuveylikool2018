using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Symptom
    {
        public int SymptomId { get; set; }
        [Required]
        [MaxLength(120)]
        public string SymptomName { get; set; }
    }
}