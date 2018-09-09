using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class MemberServices : IMemberServices
    {
         public async Task<Member> GetMemberByID(long ID)
        {
           var memberById = await _skinHubAppDbContext.Member.FirstOrDefaultAsync(x=>x.ID == ID);
           if(memberById != null)
           {
               return memberById;
           }
           return null;
        }

        public async Task<Member> GetMemberByUsername(string username)
        {
            var memberByUsername = await _skinHubAppDbContext.Member.Where(x=>x.Username == username).FirstOrDefaultAsync();
            if(memberByUsername != null)
            {
                return memberByUsername;
            }
            return null;
        }
        
        private readonly SkinHubAppDbContext _skinHubAppDbContext;
        public MemberServices(SkinHubAppDbContext  skinHubAppDbContext)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
        }

        public async Task<Member> Login(string username, string emailAddress, string password)
        {
           try
           {
               //Get the Member
               var logMember = await _skinHubAppDbContext.Member.Where(x=>x.Username == username || x.EmailAddress == emailAddress)
                            .FirstOrDefaultAsync() ;
               if(logMember == null)
               {
                   return null;
               }
                    
            //check password
               if(!VerifyPasswordHash(password, logMember.PasswordHash, logMember.PasswordSalt))
                   {
                        return null;
                   }
            //Authentication is succefull
                return logMember; 
           }
           catch (Exception)
           {
              return null;
           }

        }
       private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
       {
           using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
           {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for(int i = 0; i < computedHash.Length; i++)
               {
                  if(computedHash[i] != passwordHash[i])
                        return false;
               }
               return true;
           }
       }
// MEMBER REGISTRATION ACTION
        public async Task<Member> Register(Member register, string password)
        {
            //PASSWORD ENCRYPTION
            byte[] passwordHash, passwordSalt;
            CreatePasswordEncrypt(password, out passwordHash, out passwordSalt);

            register.PasswordHash = passwordHash;
            register.PasswordSalt = passwordSalt;

            await _skinHubAppDbContext.Member.AddAsync(register);
            await _skinHubAppDbContext.SaveChangesAsync();
            return register;
        }
//CREATION OF PASSWORD ENCRYPTION
        private void CreatePasswordEncrypt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using(var hmac = new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

//CHECKING IF Member EXIST ALREADY
        public async Task<bool> MemberExists(string username, string emailAddress)
        {
            if(await _skinHubAppDbContext.Member
                        .AnyAsync(x => x.Username == username && x.EmailAddress == emailAddress))
            {
                return true;
            }
            return false;
        }
//DISPOSING ALL INSTANCES CREATED 
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
        // ~MemberServices() {
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
    }
}