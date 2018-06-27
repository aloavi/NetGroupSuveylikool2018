using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;

namespace WebClient.ViewModels
{
    public class DiseaseCreateEditViewModel
    {
        public Disease Disease { get; set; }
        public MultiSelectList SymtomsSelectList { get; set; }
    }
}