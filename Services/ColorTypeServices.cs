using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class ColorTypeServices : IColorTypeServices
    {
         #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
        #endregion


        #region Ctor
        public ColorTypeServices(SkinHubAppDbContext skinHubAppDbContext)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
        }
        #endregion



        #region Methods
        public async Task<int> CreateColorType(CreateColorTypeDto model)
        {
            if(model != null)
            {
                var data = new ColorType
                  {
                      Name = model.Name,
                      GenderTypeID = model.GenderTypeID 
                  };
                  await _skinHubAppDbContext.AddAsync(data);
                  await _skinHubAppDbContext.SaveChangesAsync();
                  return data.ID;
            }
            return 0;
             
        }

        public async Task<IEnumerable<ColorTypeDto>> GetAllColorTypes()
        {
            var colorType = await _skinHubAppDbContext.ColorType.Include(c => c.GenderType).ToListAsync();
            if(colorType.Count() > 0)
            {
                var model = new List<ColorTypeDto>();
                model.AddRange(colorType.OrderBy(g => g.ID).Select(m => new ColorTypeDto()
                {
                    ID = m.ID,
                    Name = m.Name ,
                    GenderTypeID = m.GenderTypeID,
                    GenderType = m.GenderType.Name
                }));
                return model;
            }
            return null;
        }

        public async Task<ColorTypeDto> GetColorTypeByID(int id)
        {
            var colorTypeByID = await _skinHubAppDbContext.ColorType.Include(c => c.GenderType).FirstOrDefaultAsync(g => g.ID == id);
            if(colorTypeByID  != null)
            {
                var model = new ColorTypeDto
                {
                    ID = colorTypeByID.ID,
                    Name = colorTypeByID.Name,
                    GenderTypeID = colorTypeByID.GenderTypeID,
                    GenderType = colorTypeByID.GenderType.Name,
                };
                return model;
            }
            return null;
        }

         public async Task<IEnumerable<ColorTypeDto>> GetColorTypesByGenderTypeID(int id)
        {
            var colorTypeByGenderID = await _skinHubAppDbContext.ColorType.Include(c =>c.GenderType).Where(g => g.GenderTypeID == id).ToListAsync();
            if(colorTypeByGenderID.Count() > 0)
            {
                var model = new List<ColorTypeDto>();
                model.AddRange(colorTypeByGenderID.OrderBy(o => o.ID).Select( n => new ColorTypeDto()
                {
                    
                    ID = n.ID,
                    Name = n.Name,
                    GenderTypeID = n.GenderTypeID,
                    GenderType = n.GenderType.Name,
                
                }));
                
                return model;
            }
            return null;
        }

        public async Task<int> UpdateColorType(ColorTypeDto model)
        {
            var colorToUpdate = await _skinHubAppDbContext.ColorType.FindAsync(model.ID);
            if(colorToUpdate != null)
            {
                colorToUpdate.Name = model.Name;

                _skinHubAppDbContext.Entry(colorToUpdate).State = EntityState.Modified;
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
            if(await _skinHubAppDbContext.ColorType.AnyAsync(c => c.Name == name && c.GenderTypeID == id))
                return true;
            return false;
        }

        #endregion
    }
}