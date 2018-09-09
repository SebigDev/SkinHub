using System;

namespace SkinHubApp.Models
{
    public class Reply
    {
        public int ID {get; set;}

        public string ReplyBody {get; set;}

        public DateTime CreatedOn {get; set;} = DateTime.UtcNow;

        public string Author {get; set;}

        public long CommentID {get; set;}

        public virtual Comment Comment {get; set;}
    }
}