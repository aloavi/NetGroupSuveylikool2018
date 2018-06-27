using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.App.Interfaces;
using Domain;

namespace BLL.Services
{
    public class DiagnoseService : IDiagnoseService
    {
        private readonly IAppUnitOfWork _uow;

        public DiagnoseService(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Takes in a list of symptoms and returns diseases with matching symptoms.
        /// The diseases must prsent all symtoms. If no disease with the combination of symptoms
        /// is found an empty list is returned. 
        /// </summary>
        /// <param name="symptoms">List of symptoms</param>
        /// <returns>A list of matching diseases</returns>
        public async Task<List<DiseaseDTO>> DiagnoseAsync(List<SymptomDTO> symptoms)
        {
            List<Disease> diseases = await _uow.Diseases.GetBySymptomsAsync(symptoms
                .Select(SymptomDTO.CreateFromDTO)
                .ToList());

            return diseases.Select(DiseaseDTO.CreateFromDomainWithDiseases).ToList();
        }

        /// <summary>
        /// Returns a questionnaire object containing the symptom that needs a YES/NO answer.
        /// Used to start the Questionnaire diagnosis.
        /// </summary>
        /// <returns>Questionnaire with a new question or the answer</returns>
        public async Task<Questionnaire> DiagnoseInteractiveAsync()
        {
            return await DiagnoseInteractiveAsync(new Questionnaire());
        }

        /// <summary>
        /// Returns all the previous answers along with a questionnaire object
        /// containing the symptom that needs a YES/NO answer or the resulting disease.
        /// </summary>
        /// <param name="questionnaire">Previously answered questions</param>
        /// <returns>Questionnaire with a new question or the answer</returns>
        public async Task<Questionnaire> DiagnoseInteractiveAsync(Questionnaire questionnaire)
        {
            if (questionnaire.NewQuestion != null)
                questionnaire.PreviousAnswers.Append(questionnaire.NewQuestion);

            questionnaire.NewQuestion = new Question() { SymptomDTO = await GetSymptomForQuestionAsync(questionnaire.PreviousAnswers) };
            return questionnaire;
        }

        private async Task<SymptomDTO> GetSymptomForQuestionAsync(List<Question> questions)
        {

            // TODO All this is wrong. Get Diseases and filter those instead. Taking a Break
            // Get all symptoms
            var dbSymptoms = await _uow.Symptoms.GetSymptomsWithDiseases();
            // Turn them into queriable
            var symptoms = dbSymptoms.AsQueryable();
            // Filter based on already ansewered questions
            if (questions != null)
            {
                foreach (var question in questions)
                {
                    if (question.Answer == null) return null; // Should not be possible. All questions must be answered
                    symptoms = question.Answer.Value
                        ? symptoms.Where(s => s.SymptomId == question.SymptomDTO.SymptomId)
                        : symptoms.Where(s => s.SymptomId != question.SymptomDTO.SymptomId);
                }
            }

            var x = symptoms.GroupBy(x => x.Diseases);

            return symptoms.Select(SymptomDTO.CreateFromDomain).FirstOrDefault();
        }
    }
}