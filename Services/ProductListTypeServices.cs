using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class ProductListTypeServices : IProductListTypeServices
    {
        #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
        #endregion


        #region Ctor
        public ProductListTypeServices(SkinHubAppDbContext skinHubAppDbContext)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
        }
        #endregion



        #region Methods
        public async Task<int> CreateProductListType(CreateProductListTypeDto model)
        {
            if(model != null)
            {
                var data = new ProductListType
                  {
                      Name = model.Name,
                      ProductTypeID = model.ProductTypeID
                  };
                  await _skinHubAppDbContext.AddAsync(data);
                  await _skinHubAppDbContext.SaveChangesAsync();
                  return data.ID;
            }
            return 0;
             
        }

        public async Task<IEnumerable<ProductListTypeDto>> GetAllProductListTypes()
        {
            var productType = await _skinHubAppDbContext.ProductListType.Include(c => c.ProductType).ToListAsync();
            if(productType.Count() > 0)
            {
                var model = new List<ProductListTypeDto>();
                model.AddRange(productType.OrderBy(g => g.ID).Select(m => new ProductListTypeDto()
                {
                    ID = m.ID,
                    Name = m.Name ,
                    ProductTypeID = m.ProductTypeID,
                    ProductType = m.ProductType.Name
                }));
                return model;
            }
            return null;
        }

        public async Task<ProductListTypeDto> GetProductListTypeByID(int id)
        {
            var productTypeByID = await _skinHubAppDbContext.ProductListType.Include(c => c.ProductType).FirstOrDefaultAsync(g => g.ID == id);
            if(productTypeByID  != null)
            {
                var model = new ProductListTypeDto
                {
                    ID = productTypeByID.ID,
                    Name = productTypeByID.Name,
                    ProductTypeID = productTypeByID.ProductTypeID,
                    ProductType = productTypeByID.ProductType.Name,
                };
                return model;
            }
            return null;
        }

         public async Task<IEnumerable<ProductListTypeDto>> GetProductListTypesByProductTypeID(int id)
        {
            var productTypeByColorID = await _skinHubAppDbContext.ProductListType.Include(c =>c.ProductType).Where(g => g.ProductTypeID == id).ToListAsync();
            if(productTypeByColorID.Count() > 0)
            {
                var model = new List<ProductListTypeDto>();
                model.AddRange(productTypeByColorID.OrderBy(o => o.ID).Select( n => new ProductListTypeDto()
                {
                    
                    ID = n.ID,
                    Name = n.Name,
                    ProductTypeID = n.ProductTypeID,
                    ProductType = n.ProductType.Name
                
                }));
                
                return model;
            }
            return null;
        }

        public async Task<int> UpdateProductListType(ProductListTypeDto model)
        {
            var productToUpdate = await _skinHubAppDbContext.ProductType.FindAsync(model.ID);
            if(productToUpdate != null)
            {
                productToUpdate.Name = model.Name;

                _skinHubAppDbContext.Entry(productToUpdate).State = EntityState.Modified;
                await _skinHubAppDbContext.SaveChangesAsync();
                return model.ID;
            }
            return 0;
        }



        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GenderTypeServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


        #region Validation
        public async Task<bool> IsNameExist(string name, int id)
        {
            if(await _skinHubAppDbContext.ProductListType.AnyAsync(c => c.Name == name && c.ProductTypeID == id))
                return true;
            return false;
        }

        #endregion
    }
}