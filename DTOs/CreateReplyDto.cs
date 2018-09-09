using System;

namespace SkinHubApp.DTOs
{
    public class CreateReplyDto
    {

        public string ReplyBody {get; set;}

        public DateTime CreatedOn {get; set;} = DateTime.UtcNow;

        public string Author {get; set;}

        public long CommentID {get; set;}
    }
}