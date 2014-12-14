namespace Bloghost.Model
{
    public class Comment : Entity
    {
        public static class MaxLengthFor
        {
            public const int CONTENT = 1000;
        }

        public string Content { get; set; }
        public virtual Post Post { get; set; }
        public virtual Comment ParentCommet { get; set; }
        public virtual User User { get; set; }

        public Comment()
        {
            Content = string.Empty;
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
