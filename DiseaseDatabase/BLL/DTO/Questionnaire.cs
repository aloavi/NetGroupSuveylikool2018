using System.Collections.Generic;
using System.Reflection.Metadata;

namespace BLL.DTO
{
    public class Questionnaire
    {
        public List<Question> PreviousAnswers { get; set; } = new List<Question>();
        public Question NewQuestion { get; set; }
        public DiseaseDTO Result { get; set; }
    }

    public class Question
    {
        public SymptomDTO Symptom { get; set; }
        public bool? Answer { get; set; }
    }
}