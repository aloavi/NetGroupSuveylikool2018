using System.Collections.Generic;

namespace WebClient.Models
{
    public class Questionnaire
    {
        public List<Question> PreviousAnswers { get; set; } = new List<Question>();
        public Question NewQuestion { get; set; }
        public Disease Result { get; set; }
    }

    public class Question
    {
        public Symptom Symptom { get; set; }
        public bool? Answer { get; set; }
    }
}