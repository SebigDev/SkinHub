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
    public sealed class MemberServices : IMemberServices
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
               return !VerifyPasswordHash(password, logMember.PasswordHash, logMember.PasswordSalt) ? null : logMember;
            //Authentication is successful
           }
           catch (Exception)
           {
              return null;
           }

        }
       private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
       {
           using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
           {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
           }

       }
// MEMBER REGISTRATION ACTION
        public async Task<Member> Register(Member register, string password)
        {
            //PASSWORD ENCRYPTION
            CreatePasswordEncrypt(password, out var passwordHash, out var passwordSalt);

            register.PasswordHash = passwordHash;
            register.PasswordSalt = passwordSalt;

            await _skinHubAppDbContext.Member.AddAsync(register);
            await _skinHubAppDbContext.SaveChangesAsync();
            return register;
        }
//CREATION OF PASSWORD ENCRYPTION
        private static void CreatePasswordEncrypt(string password, out byte[] passwordHash, out byte[] passwordSalt)
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
            return await _skinHubAppDbContext.Member
                .AnyAsync(x => x.Username == username && x.EmailAddress == emailAddress);
        }


            //Get Methods
         public async Task<MemberDto> GetMemberById(long id)
        {
           var memberById = await _skinHubAppDbContext.Member.Where(x =>x.ID == id).FirstOrDefaultAsync();
            if (memberById == null) return null;
            var model = new MemberDto
            {
                ID = memberById.ID,
                Username = memberById.Username,
                EmailAddress = memberById.EmailAddress,
                Color = memberById.Color,
                Gender = memberById.Gender,
                Firstname = memberById.Firstname,
                Middlename = memberById.Middlename,
                Lastname = memberById.Lastname,
                DateOfBirth = memberById.DateOfBirth,
                Age = memberById.Age
            };
            return model;
        }

        public async Task<MemberDto> GetMemberByUsername(string username)
        {
            var memberByUsername = await _skinHubAppDbContext.Member.Where(x=>x.Username == username).FirstOrDefaultAsync();
            if (memberByUsername == null) return null;
            var model = new MemberDto
            {
                ID = memberByUsername.ID,
                Username = memberByUsername.Username,
                EmailAddress = memberByUsername.EmailAddress,
                Color = memberByUsername.Color,
                Gender = memberByUsername.Gender,
                Firstname = memberByUsername.Firstname,
                Middlename = memberByUsername.Middlename,
                Lastname = memberByUsername.Lastname,
                DateOfBirth = memberByUsername.DateOfBirth,
                Age = memberByUsername.Age
            };
            return model;
        }

         public async Task<IEnumerable<MemberDto>> GetMemberByColor(string color)
        {
            var memberByUsername = await _skinHubAppDbContext.Member.Where(x=>x.Color.ToString() == color).ToListAsync();
            if (memberByUsername == null) return null;
            var model = new List<MemberDto>();
            model.AddRange(memberByUsername.OrderBy(c =>c.ID).Select(m => new MemberDto()
            {
                ID = m.ID,
                Username = m.Username,
                EmailAddress = m.EmailAddress,
                Color = m.Color,
                Gender = m.Gender,
                Firstname = m.Firstname,
                Middlename = m.Middlename,
                Lastname = m.Lastname,
                DateOfBirth = m.DateOfBirth,
                Age = m.Age
            }));
            return model;
        }

            public async Task<long> UpdateMember(MemberDto update)
            {
                
                var memberToUpdate = await _skinHubAppDbContext.Member.FirstOrDefaultAsync(s =>s.ID == update.ID);
                if (memberToUpdate == null) return 0;
                memberToUpdate.Firstname = update.Firstname;
                memberToUpdate.Lastname = update.Lastname;
                memberToUpdate.Middlename = update.Middlename;
                memberToUpdate.DateOfBirth = update.DateOfBirth;

                _skinHubAppDbContext.Entry(memberToUpdate).State = EntityState.Modified;
                await _skinHubAppDbContext.SaveChangesAsync();
                return update.ID;
            }


//DISPOSING ALL INSTANCES CREATED 
        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposedValue = true;
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