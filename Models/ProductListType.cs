namespace SkinHubApp.Models
{
    public class ProductListType
    {
        public int ID {get; set;}

        public string Name {get; set;}

        public int ProductTypeID {get; set;} 

        public virtual ProductType ProductType {get; set;}
    }
}