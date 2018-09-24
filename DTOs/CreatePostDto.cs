using System;

namespace SkinHubApp.DTOs
{
    public class CreatePostDto
    {
        
        public string Title {get; set;}

        public string Body {get; set;}

        public int ProductListTypeID {get; set;}

        public DateTime CreatedOn {get; set;} = DateTime.UtcNow;


    }
}