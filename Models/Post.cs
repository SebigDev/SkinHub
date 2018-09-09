using System;
using System.Collections.Generic;

namespace SkinHubApp.Models
{
    public class Post
    {
        public long ID {get; set;}
        
        public string Title {get; set;}

        public string Body {get; set;}

        public  string Author {get; set;} = "Annonymous";

        public DateTime CreatedOn {get; set;} = DateTime.UtcNow;

        public int ProductListTypeID {get; set;}

        public virtual ProductListType ProductListType {get; set;}

        public ICollection<Comment> Comment {get; set;}


    }
}