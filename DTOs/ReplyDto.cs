using System;

namespace SkinHubApp.DTOs
{
    public class ReplyDto
    {
         public int ID {get; set;}

        public string ReplyBody {get; set;}

        public DateTime CreatedOn {get; set;} 

        public string Author {get; set;}

        public long CommentID {get; set;}

        public string Comment {get; set;}
    }
}