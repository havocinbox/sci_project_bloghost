using Bloghost.Logic.Components.Services;
using System;

namespace Bloghost.Logic.Components
{
    public static class ServiceFactory
    {
        private static readonly Lazy<IUserService> _userService = new Lazy<IUserService>(() =>
            new UserService(DaoFactoryProvider.GetDaoFactory().GetUserDao()));

        private static readonly Lazy<IBlogService> _blogaService = new Lazy<IBlogService>(() =>
            new BlogService(DaoFactoryProvider.GetDaoFactory().GetBlogDao()));

        private static readonly Lazy<IPostService> _postService = new Lazy<IPostService>(() =>
            new PostService(DaoFactoryProvider.GetDaoFactory().GetPostDao()));

        private static readonly Lazy<ICommentService> _commentService = new Lazy<ICommentService>(() =>
            new CommentService(DaoFactoryProvider.GetDaoFactory().GetCommentDao()));

        private static readonly Lazy<ITagSerivce> _tagService = new Lazy<ITagSerivce>(() =>
            new TagService(DaoFactoryProvider.GetDaoFactory().GetTagDao()));

        private static readonly Lazy<IMembershipService> _membershipService = new Lazy<IMembershipService>(() =>
           new MembershipService(_userService.Value));

        private static readonly Lazy<IRoleService> _roleService = new Lazy<IRoleService>(() =>
           new RoleService(DaoFactoryProvider.GetDaoFactory().GetRoleDao()));

        public static IUserService GetUserService()
        {
            return _userService.Value;
        }

        public static IBlogService GetBlogService()
        {
            return _blogaService.Value;
        }

        public static IPostService GetPostService()
        {
            return _postService.Value;
        }

        public static ICommentService GetCommentService()
        {
            return _commentService.Value;
        }

        public static ITagSerivce GetTagService()
        {
            return _tagService.Value;
        }

        public static IMembershipService GetMembershipService()
        {
            return _membershipService.Value;
        }

        public static IRoleService GetRoleService()
        {
            return _roleService.Value;
        }
    }
}
