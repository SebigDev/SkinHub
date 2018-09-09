using System.Collections.Generic;

namespace SkinHubApp.Models
{
    public class GenderType
    {
        public int ID {get; set;}

        public string Name {get; set;}
        
        public ICollection<ColorType> ColorType {get; set;}
    }
}