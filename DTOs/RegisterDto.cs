using System;
using SkinHubApp.Enum;

namespace SkinHubApp.DTOs
{
    public class RegisterDto
    {

        public string Username {get; set;}

        public string EmailAddress {get; set;}

        public string Password {get; set;}

        public string Firstname {get; set;}

        public string Middlename {get; set;}

        public string  Lastname {get; set;}

        public string Color {get; set;}

        public GenderEnum Gender {get; set;}

        public DateTime DateOfBirth {get; set;}

     
    }
}