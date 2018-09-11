using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class MemberServices : IMemberServices
    {
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


        //password Change

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


            //Get Methods
         public async Task<MemberDto> GetMemberByID(long ID)
        {
           var memberById = await _skinHubAppDbContext.Member.Include(x =>x.Color).Where(x =>x.ID == ID).FirstOrDefaultAsync();
           if(memberById != null)
           {
               var model = new MemberDto
               {
                   ID = memberById.ID,
                    Username = memberById.Username,
                    EmailAddress = memberById.EmailAddress,
                    ColorTypeID = memberById.ColorTypeID,
                    Color = memberById.Color.Name,
                    Gender = memberById.Gender,
                    Firstname = memberById.Firstname,
                    Middlename = memberById.Middlename,
                    Lastname = memberById.Lastname,
                    DateOfBirth = memberById.DateOfBirth,
                    Age = memberById.Age
               };
               return model;
           }
           return null;
        }

        public async Task<MemberDto> GetMemberByUsername(string username)
        {
            var memberByUsername = await _skinHubAppDbContext.Member.Include(x =>x.Color).Where(x=>x.Username == username).FirstOrDefaultAsync();
            if(memberByUsername != null)
            {
                var model = new MemberDto
               {
                   ID = memberByUsername.ID,
                    Username = memberByUsername.Username,
                    EmailAddress = memberByUsername.EmailAddress,
                    ColorTypeID = memberByUsername.ColorTypeID,
                    Color = memberByUsername.Color.Name,
                    Gender = memberByUsername.Gender,
                    Firstname = memberByUsername.Firstname,
                    Middlename = memberByUsername.Middlename,
                    Lastname = memberByUsername.Lastname,
                    DateOfBirth = memberByUsername.DateOfBirth,
                    Age = memberByUsername.Age
               };
               return model;
            }
            return null;
        }

         public async Task<IEnumerable<MemberDto>> GetMemberByColorID(int id)
        {
            var memberByUsername = await _skinHubAppDbContext.Member.Include(x =>x.Color).Where(x=>x.ColorTypeID == id).ToListAsync();
            if(memberByUsername != null)
            {
                var model = new List<MemberDto>();
                model.AddRange(memberByUsername.OrderBy(c =>c.ID).Select(m => new MemberDto()
               {
                    ID = m.ID,
                    Username = m.Username,
                    EmailAddress = m.EmailAddress,
                    ColorTypeID = m.Color.ID,
                    Color = m.Color.Name,
                    Gender = m.Gender,
                    Firstname = m.Firstname,
                    Middlename = m.Middlename,
                    Lastname = m.Lastname,
                    DateOfBirth = m.DateOfBirth,
                    Age = m.Age
               }));
               return model;
            }
            return null;
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