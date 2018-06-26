﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ISymptomService
    {
        Task<int> SymptomCountAsync();
        Task<List<SymptomDTO>> GetTopSymptomsAsync(int ammount = 3);
    }
}