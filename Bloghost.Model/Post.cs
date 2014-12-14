using System.Collections.Generic;
using System.Linq;

namespace Bloghost.Model
{
    public class Post : Entity
    {
        public static class MaxLengthFor
        {
            public const int TITLE = 100;
            public const int CONTENT = 100000;
        }

        public string Title { get; set; }
        public string AccessName { get; set; }
        public string Content { get; set; }
        public virtual IList<Tag> Tags { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual Blog Blog { get; set; }
        public int Rating { get; set; }

        public Post()
        {
            Title = string.Empty;
            Content = string.Empty;
            Rating = 0;
        }

        public bool HasTag(Tag tag)
        {
            return Tags.Any(x => x.Equals(tag));
        }

        public override string ToString()
        {
            return Title + '\n' + Content;
        }
    }
}
