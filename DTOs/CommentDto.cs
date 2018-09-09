using System;

namespace SkinHubApp.DTOs
{
    public class CommentDto
    {
        public long ID {get; set;}

        public string CommentBody{get; set;}

        public string Author {get; set;}

        public DateTime CreatedOn{get; set;} 

        public long PostID {get; set;}

        public string Post {get; set;}

       // public ICollection<Reply> Reply {get; set;}
    }
}