using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class ProductTypeServices : IProductTypeServices
    {
         #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
        #endregion


        #region Ctor
        public ProductTypeServices(SkinHubAppDbContext skinHubAppDbContext)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
        }
        #endregion



        #region Methods
        public async Task<int> CreateProductType(CreateProductTypeDto model)
        {
            if(model != null)
            {
                var data = new ProductType
                  {
                      Name = model.Name,
                      ColorTypeID = model.ColorTypeID
                  };
                  await _skinHubAppDbContext.AddAsync(data);
                  await _skinHubAppDbContext.SaveChangesAsync();
                  return data.ID;
            }
            return 0;
             
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllProductTypes()
        {
            var productType = await _skinHubAppDbContext.ProductType.Include(c => c.ColorType).ToListAsync();
            if(productType.Count() > 0)
            {
                var model = new List<ProductTypeDto>();
                model.AddRange(productType.OrderBy(g => g.ID).Select(m => new ProductTypeDto()
                {
                    ID = m.ID,
                    Name = m.Name ,
                    ColorTypeID = m.ColorTypeID,
                    ColorType = m.ColorType.Name
                }));
                return model;
            }
            return null;
        }

        public async Task<ProductTypeDto> GetProductTypeByID(int id)
        {
            var productTypeByID = await _skinHubAppDbContext.ProductType.Include(c => c.ColorType).FirstOrDefaultAsync(g => g.ID == id);
            if(productTypeByID  != null)
            {
                var model = new ProductTypeDto
                {
                    ID = productTypeByID.ID,
                    Name = productTypeByID.Name,
                    ColorTypeID = productTypeByID.ColorTypeID,
                    ColorType = productTypeByID.ColorType.Name,
                };
                return model;
            }
            return null;
        }

         public async Task<IEnumerable<ProductTypeDto>> GetProductTypesByColorTypeID(int id)
        {
            var productTypeByColorID = await _skinHubAppDbContext.ProductType.Include(c =>c.ColorType).Where(g => g.ColorTypeID == id).ToListAsync();
            if(productTypeByColorID.Count() > 0)
            {
                var model = new List<ProductTypeDto>();
                model.AddRange(productTypeByColorID.OrderBy(o => o.ID).Select( n => new ProductTypeDto()
                {
                    
                    ID = n.ID,
                    Name = n.Name,
                   ColorTypeID = n.ColorTypeID,
                    ColorType = n.ColorType.Name
                
                }));
                
                return model;
            }
            return null;
        }

        public async Task<int> UpdateProductType(ProductTypeDto model)
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
            if(await _skinHubAppDbContext.ProductType.AnyAsync(c => c.Name == name && c.ColorTypeID == id))
                return true;
            return false;
        }

        #endregion
    }
}