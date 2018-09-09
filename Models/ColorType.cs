using System.Collections.Generic;

namespace SkinHubApp.Models
{
    public class ColorType
    {
        public int ID {get; set;}

        public string Name {get; set;}

        public int GenderTypeID {get; set;}

        public virtual GenderType GenderType {get; set;}

        public ICollection<ProductType> ProductType {get; set;}
    }
}