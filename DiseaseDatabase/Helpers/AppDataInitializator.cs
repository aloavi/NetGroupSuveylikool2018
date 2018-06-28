using System;
using System.Collections.Generic;
using System.IO;
using DAL.App.EF;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces;
using Domain;

namespace Helpers
{
    public class AppDataInitializator : IAppDataInitializator
    {
        private readonly IAppUnitOfWork _uow;

        public AppDataInitializator(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ClearDb()
        {
            _uow.DiseaseSymptoms.Clear();
            _uow.Diseases.Clear();
            _uow.Symptoms.Clear();
            await _uow.SaveChangesAsync();
        }

        public async Task InitializeDbAsync()
        {
            if (await _uow.Diseases.CountAsync() > 0 || await _uow.Symptoms.CountAsync() > 0) return;

            await InitailizeFromDefaultAsync();
        }

        public async Task InitializeDbAsync(string[] csv)
        {
            if (await _uow.Diseases.CountAsync() > 0 || await _uow.Symptoms.CountAsync() > 0) return;

            await InitailizeFromStringAsync(csv);
        }

        private async Task InitailizeFromStringAsync(string[] csv)
        {
            foreach (var line in csv)
            {
                await AddDiseaseFromString(line);
            }
        }

        private async Task InitailizeFromDefaultAsync()
        {
            using (var reader = new StreamReader(@"database.csv"))
            {
                while (!reader.EndOfStream)
                {
                    await AddDiseaseFromString(reader.ReadLine());
                }
            }
        }

        private async Task AddDiseaseFromString(string line)
        {
            if (line != null)
            {
                var values = line.Split(", ");
                var disease = new Disease() { DiseaseName = values[0] };
                await _uow.Diseases.AddAsync(disease);

                foreach (var name in values.Skip(1))
                {
                    var symptom = await _uow.Symptoms.FindByNameAsync(name);
                    if (symptom == null)
                    {
                        symptom = new Symptom() { SymptomName = name };
                        await _uow.Symptoms.AddAsync(symptom);
                    }
                    await _uow.DiseaseSymptoms.AddAsync(new DiseaseSymptom() { Disease = disease, Symptom = symptom });
                }
            }
            await _uow.SaveChangesAsync();
        }
    }
}