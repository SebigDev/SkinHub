using System.Collections.Generic;
using SkinHubApp.Models;

namespace SkinHubApp.DTOs
{
    public class ProductTypeDto
    {
         public int ID {get; set;}

        public string Name {get; set;}

        public int ColorTypeID {get; set;}

        public string ColorType {get; set;}

       // public ICollection<ProductListType> ProductListType {get; set;}
    }
}