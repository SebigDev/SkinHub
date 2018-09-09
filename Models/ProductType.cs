using System.Collections.Generic;

namespace SkinHubApp.Models
{
    public class ProductType
    {
        public int ID {get; set;}

        public string Name {get; set;}

        public int ColorTypeID {get; set;}

        public virtual ColorType ColorType {get; set;}

        public ICollection<ProductListType> ProductListType {get; set;}
    }
}