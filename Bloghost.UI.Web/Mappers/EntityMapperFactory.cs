using System;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public static class EntityMapperFactory
    {
        private static readonly Lazy<IEntityMapper<ViewUserModel, User>> UserMapper =
            new Lazy<IEntityMapper<ViewUserModel, User>>(() => new ViewUserMapper());

        private static readonly Lazy<IEntityMapper<RegisterUserModel, User>> RegistrationUserMapper =
            new Lazy<IEntityMapper<RegisterUserModel, User>>(() => new RegisterUserMapper());

        private static readonly Lazy<IEntityMapper<EditBlogModel, Blog>> EditBlogMapper =
            new Lazy<IEntityMapper<EditBlogModel, Blog>>(() => new EditBlogMapper());

        private static readonly Lazy<IEntityMapper<ViewBlogModel, Blog>> BlogDescriptionMapper =
            new Lazy<IEntityMapper<ViewBlogModel, Blog>>(() => new ViewBlogMapper());

        private static readonly Lazy<IEntityMapper<ViewBlogListModel, Blog>> BlogListMapper =
            new Lazy<IEntityMapper<ViewBlogListModel, Blog>>(() => new ViewBlogListMapper());

        private static readonly Lazy<IEntityMapper<ViewCommentModel, Comment>> CommentMapper =
            new Lazy<IEntityMapper<ViewCommentModel, Comment>>(() => new ViewCommentMapper());

        private static readonly Lazy<IEntityMapper<ViewPostModel, Post>> PostMapper =
            new Lazy<IEntityMapper<ViewPostModel, Post>>(() => new ViewPostMapper());

        private static readonly Lazy<IEntityMapper<ViewPostListModel, Blog>> PostListMapper =
            new Lazy<IEntityMapper<ViewPostListModel, Blog>>(() => new ViewPostListMapper());

        private static readonly Lazy<IEntityMapper<EditPostModel, Post>> EditPostMapper =
            new Lazy<IEntityMapper<EditPostModel, Post>>(() => new EditPostMapper());

        private static readonly Lazy<IEntityMapper<EditCommentModel, Comment>> EditCommentMapper =
            new Lazy<IEntityMapper<EditCommentModel, Comment>>(() => new EditCommentMapper());

        public static IEntityMapper<ViewUserModel, User> GetViewUserMapper()
        {
            return UserMapper.Value;
        }

        public static IEntityMapper<RegisterUserModel, User> GetRegisterUserMapper()
        {
            return RegistrationUserMapper.Value;
        }

        public static IEntityMapper<EditBlogModel, Blog> GetEditBlogMapper()
        {
            return EditBlogMapper.Value;
        }

        public static IEntityMapper<ViewBlogModel, Blog> GetViewBlogMapper()
        {
            return BlogDescriptionMapper.Value;
        }

        public static IEntityMapper<ViewBlogListModel, Blog> GetViewBlogListMapper()
        {
            return BlogListMapper.Value;
        }

        public static IEntityMapper<ViewCommentModel, Comment> GetViewCommentMapper()
        {
            return CommentMapper.Value;
        }

        public static IEntityMapper<ViewPostModel, Post> GetViewPostMapper()
        {
            return PostMapper.Value;
        }

        public static IEntityMapper<ViewPostListModel, Blog> GetViewPostListMapper()
        {
            return PostListMapper.Value;
        }

        public static IEntityMapper<EditPostModel, Post> GetEditPostMapper()
        {
            return EditPostMapper.Value;
        }

        public static IEntityMapper<EditCommentModel, Comment> GetEditCommentMapper()
        {
            return EditCommentMapper.Value;
        }
    }
}