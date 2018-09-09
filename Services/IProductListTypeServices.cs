using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface IProductListTypeServices
    {
        Task<int> CreateProductListType(CreateProductListTypeDto model);

        Task<int> UpdateProductListType(ProductListTypeDto model);

        Task<IEnumerable<ProductListTypeDto>> GetAllProductListTypes();

        Task<ProductListTypeDto> GetProductListTypeByID(int id);

        Task<IEnumerable<ProductListTypeDto>> GetProductListTypesByProductTypeID(int id);

        Task<bool> IsNameExist(string name, int id);
    }
}