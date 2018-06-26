namespace Domain
{
    public class DiseaseSymptom
    {
        public int DiseaseSymptomId { get; set; }
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}