using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface IColorTypeServices : IDisposable
    {
        Task<int> CreateColorType(CreateColorTypeDto model);

        Task<int> UpdateColorType(ColorTypeDto model);

        Task<IEnumerable<ColorTypeDto>> GetAllColorTypes();

        Task<ColorTypeDto> GetColorTypeByID(int id);

        Task<IEnumerable<ColorTypeDto>> GetColorTypesByGenderTypeID(int id);

        
        Task<bool> IsNameExist(string name, int id);
    }
}