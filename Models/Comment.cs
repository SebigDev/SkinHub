using System;
using System.Collections.Generic;

namespace SkinHubApp.Models
{
    public class Comment
    {
        public long ID {get; set;}

        public string CommentBody{get; set;}

        public string Author {get; set;}

        public DateTime CreatedOn{get; set;} = DateTime.UtcNow;

        public long PostID {get; set;}

        public virtual Post Post {get; set;}

        public ICollection<Reply> Reply {get; set;}

    }
}