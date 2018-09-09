using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface IProductTypeServices : IDisposable
    {
         Task<int> CreateProductType(CreateProductTypeDto model);

        Task<int> UpdateProductType(ProductTypeDto model);

        Task<IEnumerable<ProductTypeDto>> GetAllProductTypes();

        Task<ProductTypeDto> GetProductTypeByID(int id);

        Task<IEnumerable<ProductTypeDto>> GetProductTypesByColorTypeID(int id);

        Task<bool> IsNameExist(string name, int Id);
    }
}