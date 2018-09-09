
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface IGenderTypeServices : IDisposable
    {
        Task<int> CreateGenderType(CreateGenderTypeDto model);

        Task<int> UpdateGenderType(GenderTypeDto model);

        Task<IEnumerable<GenderTypeDto>> GetAllGenderTypes();

        Task<GenderTypeDto> GetGenderTypeByID(int id);

        Task<bool> IsNameExist(string name);
    }
}