using System;

namespace SkinHubApp.DTOs
{
     public class CreateCommentDto
    {

        public string CommentBody{get; set;}

        public string Author {get; set;}

        public DateTime CreatedOn{get; set;} = DateTime.UtcNow;

        public long PostID {get; set;}

       // public ICollection<Reply> Reply {get; set;}
    }
}