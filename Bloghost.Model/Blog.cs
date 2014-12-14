using System.Collections.Generic;
using System.Linq;

namespace Bloghost.Model
{
    public class Blog : Entity
    {
        public static class MaxLengthFor
        {
            public const int TITLE = 100;
            public const int DESCRIPTION = 1000;
        }

        public string Title { get; set; }
        public string AccessName { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public string PathToAvatar { get; set; }
        
        public double Rating
        {
            get
            {
                if (Posts == null || Posts.Count < 2)
                    return 0;
                return Posts.Sum(post => post.Rating) / Posts.Count;
            }
        }

        public Blog()
        {
            Title = string.Empty;
            Description = string.Empty;
            Title = string.Empty;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
