﻿using System;
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
                questionnaire.PreviousAnswers.Add(questionnaire.NewQuestion);

            var diseases = await FilterDiseasesAsync(questionnaire.PreviousAnswers);

            if(diseases.Count > 1)
                questionnaire.NewQuestion = new Question()
                {
                    Symptom = GetSymptomForQuestionAsync(diseases)
                };
            else if (diseases.Count == 1)
                questionnaire.Result = DiseaseDTO.CreateFromDomain(diseases.Single());
            else
                throw new ArgumentException("Previous questions don't match any disease in the database");
            return questionnaire;
        }

        private SymptomDTO GetSymptomForQuestionAsync(List<Disease> diseases)
        {
            if (!diseases.Any())
                throw new ArgumentNullException();

            var opt = diseases.Count / 2;

            // Select symptoms and group by disease Count.
            var symptoms = diseases
                .SelectMany(d => d.Symptoms)
                .GroupBy(s => s.Symptom.SymptomName)
                .GroupBy(x => x.Count());

            // Pick first closest symptom
            var symptom = symptoms
                .OrderBy(x => Math.Abs((long)x.Key - opt))
                .First()
                .ElementAt(0)
                .ElementAt(0);

            return SymptomDTO.CreateFromDomain(symptom.Symptom);
        }

        private async Task<List<Disease>> FilterDiseasesAsync(List<Question> questions)
        {
            var dbDiseases = await _uow.Diseases.GetDiseasesWithSymptomsAsync();
            // Turn them into queriable
            var diseases = dbDiseases.AsQueryable();
            // Filter based on already ansewered questions
            if (questions != null)
                foreach (var question in questions)
                {
                    if (question.Answer == null) return null; // Should not be possible. All questions must be answered TODO throw some exeption
                    diseases = question.Answer.Value
                        ? diseases.Where(d => d.Symptoms.Any(s => s.SymptomId == question.Symptom.SymptomId))
                        : diseases.Where(d => d.Symptoms.All(s => s.SymptomId != question.Symptom.SymptomId));
                }
            return diseases.ToList();
        }
    }
}