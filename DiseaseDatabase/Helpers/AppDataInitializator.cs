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

            // TODO Add custom initializer for passing CSVs trough API
            await InitailizeFromDefaultAsync();

            // Add data like this:
            //var diseases = new Disease[]
            //{
            //    new Disease(){DiseaseName = "Z"}, 
            //};
            //foreach (var d in diseases)
            //{
            //    context.Diseases.Add(d);
            //}

            //context.SaveChanges();

        }

        private async Task InitailizeFromDefaultAsync()
        {
            using (var reader = new StreamReader(@"database.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(", ");
                        var disease = new Disease() {DiseaseName = values[0]};
                        await _uow.Diseases.AddAsync(disease);

                        foreach (var name in values.Skip(1))
                        {
                            var symptom = await _uow.Symptoms.FindByNameAsync(name);
                            if (symptom == null)
                            {
                                symptom = new Symptom(){SymptomName = name};
                                await _uow.Symptoms.AddAsync(symptom);
                            }
                            await _uow.DiseaseSymptoms.AddAsync(new DiseaseSymptom(){Disease = disease, Symptom = symptom});
                        }
                    }
                    await _uow.SaveChangesAsync();
                }
            }
        }
    }
}