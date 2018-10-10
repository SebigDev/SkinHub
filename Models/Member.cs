using System;
using SkinHubApp.Enum;

namespace SkinHubApp.Models
{
    public class Member
    {
        public long ID {get; set;}

        public string Username {get; set;}

        public string EmailAddress {get; set;}

        public byte[] PasswordHash{get; set;}

        public byte[] PasswordSalt{get; set;}

        public string Firstname {get; set;}

        public string Middlename {get; set;}

        public string  Lastname {get; set;}

        public GenderEnum Gender {get; set;}

        public DateTime DateOfBirth {get; set;}

        public SkinColor Color {get; set;}

        public int Age {

            get{
                var age = ((DateTime.UtcNow.Subtract(DateOfBirth).Days)/ 365);
                return age;
            }
            set {
                value = Age;
            }
        }

    }
}