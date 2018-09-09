using System.Collections.Generic;
using SkinHubApp.Models;

namespace SkinHubApp.DTOs
{
    public class ColorTypeDto
    {
         public int ID {get; set;}

        public string Name {get; set;}

        public int GenderTypeID {get; set;}

        public string GenderType {get; set;}

       // public ICollection<ProductType> ProductType {get; set;}
    }
}