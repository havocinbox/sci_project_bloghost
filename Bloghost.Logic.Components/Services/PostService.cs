using System;
using System.Collections.Generic;
using System.Linq;
using Bloghost.Logic.Components.Validation;
using Bloghost.Model;
using Bloghost.Data;
using HtmlAgilityPack;
using Utilies;

namespace Bloghost.Logic.Components.Services
{
    public class PostService : Service<Post>, IPostService
    {
        public PostService(IDataAccessObject<Post, Guid> dataAccessObject)
            : base(dataAccessObject, new PostValidator())
        {
        }

        public string GetAccessName(Post post)
        {
            Expect.ArgumentNotNull(post, "post");
            Expect.ArgumentNotWhiteSpaceString(post.Title, "Post.Title");

            string link;
            string linkBackup = link = post.Title.ToLower().Trim().Trim(new[] { '&', ',', '.', '/', '\\', '|', '@', '=', '?' }).Replace(' ', '_').Replace("&", "_");
            var i = 0;
            while (GetAll().FirstOrDefault(x => x.AccessName == link) != null)
            {
                link = linkBackup + i;
                i++;
            }

            return link;
        }

        public string GetPreviewPost(Post post)
        {
            //var postContent = new HtmlDocument();
            //postContent.LoadHtml(post.Content);

            try
            {
                //var hrNode = postContent.DocumentNode.SelectSingleNode("//hr");
                //var hrParentNode = hrNode.ParentNode;
                //var retuls = hrParentNode.Clone();
                //var startDeleteNodes = false;
                //foreach (var node in hrParentNode.ChildNodes)
                //{
                //    if (node.Name == "hr")
                //        startDeleteNodes = true;
                //    if (startDeleteNodes)
                //    {
                //        retuls.ChildNodes.Remove(node);
                //    }
                //}

                //return retuls.ToString();


                var doc = new HtmlDocument();
                doc.LoadHtml(post.Content.Replace(@"&lt;tr/&gt;", "<hr/>"));
                if (doc.ParseErrors.Any(error => error.Code == HtmlParseErrorCode.TagNotClosed)) //можно на другое проверять
                {
                    //незакрытый тег
                }
                var hr = doc.DocumentNode.SelectSingleNode("//hr");
                if (hr == null) //не уверен вернёт null или exception
                {
                    //return inputHtml;
                }
                var parent = hr;
                do
                {
                    while (parent.NextSibling != null)
                    {
                        parent.NextSibling.Remove();
                    }

                } while ((parent = parent.ParentNode) != null);
                hr.Remove();
               return doc.DocumentNode.InnerHtml;
            }
            catch (Exception)
            {
                return post.Content;
            }
        }

        public IEnumerable<Post> GetLatestPosts()
        {
            return GetAll().OrderByDescending(x => x.CreatedDate).Take(6);
        }
    }
}
