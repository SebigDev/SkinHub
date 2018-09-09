using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class GenderTypeServices : IGenderTypeServices
    {
        #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
            private readonly ILogger<GenderTypeServices> _logger;
        #endregion


        #region Ctor
        public GenderTypeServices(SkinHubAppDbContext skinHubAppDbContext, ILogger<GenderTypeServices> logger)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
            _logger = logger;
        }
        #endregion



        #region Methods
        public async Task<int> CreateGenderType(CreateGenderTypeDto model)
        {
            if(model != null)
            {
                var data = new GenderType
                  {
                      Name = model.Name
                  };
                  await _skinHubAppDbContext.AddAsync(data);
                  await _skinHubAppDbContext.SaveChangesAsync();
                  return data.ID;
            }
            return 0;
             
        }

        public async Task<IEnumerable<GenderTypeDto>> GetAllGenderTypes()
        {
            var genderType = await _skinHubAppDbContext.GenderType.ToListAsync();
            if(genderType.Count() > 0)
            {
                var model = new List<GenderTypeDto>();
                model.AddRange(genderType.OrderBy(g => g.ID).Select(m => new GenderTypeDto()
                {
                    ID = m.ID,
                    Name = m.Name ,
                   // ColorType = m.ColorType
                }));
                return model;
            }
            return null;
        }

        public async Task<GenderTypeDto> GetGenderTypeByID(int id)
        {
            var genderTypeByID = await _skinHubAppDbContext.GenderType.FirstOrDefaultAsync(g => g.ID == id);
            if(genderTypeByID != null)
            {
                var model = new GenderTypeDto
                {
                    ID = genderTypeByID.ID,
                    Name = genderTypeByID.Name,
                   // ColorType = genderTypeByID.ColorType
                };
                return model;
            }
            return null;
        }

        public async Task<int> UpdateGenderType(GenderTypeDto model)
        {
            var genderToUpdate = await _skinHubAppDbContext.GenderType.FindAsync(model.ID);
            if(genderToUpdate != null)
            {
                genderToUpdate.Name = model.Name;

                _skinHubAppDbContext.Entry(genderToUpdate).State = EntityState.Modified;
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
        public async Task<bool> IsNameExist(string name)
        {
            if(await _skinHubAppDbContext.GenderType.AnyAsync(c => c.Name == name))
                return true;
            return false;
        }

        #endregion
    }
}