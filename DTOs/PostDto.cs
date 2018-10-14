using System;

namespace SkinHubApp.DTOs
{
    public class PostDto
    {
         public long ID {get; set;}
        
        public string Title {get; set;}

        public string Body {get; set;}

        public  string Author {get; set;} 

        public DateTime CreatedOn {get; set;} 

        public int ProductListTypeID {get; set;}

        public string ProductListType { get; set; } = "none";

    }
}