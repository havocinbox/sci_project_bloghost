using System;
using System.Collections.Generic;

namespace Bloghost.Model
{
    public class Tag : Entity
    {
        public static class MaxLengthFor
        {
            public const int CONTENT = 50;
        }

        public string Content { get; set; }
        public virtual IList<Post> Posts { get; set; }

        public Tag()
        {
            Content = string.Empty;
        }        

        public override string ToString()
        {
            return Content;
        }
    }
}
