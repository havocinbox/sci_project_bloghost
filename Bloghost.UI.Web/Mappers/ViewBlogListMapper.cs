using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bloghost.Logic.Components;
using Bloghost.Model;
using Bloghost.UI.Web.Models;

namespace Bloghost.UI.Web.Mappers
{
    public class ViewBlogListMapper : EntityMapperBase<ViewBlogListModel, Blog>
    {
        protected override ViewBlogListModel OnMap(Blog entity)
        {
            var model = new ViewBlogListModel();
            model.BlogList = new List<ViewBlogModel>(20);
            foreach (var blog in ServiceFactory.GetBlogService().GetAll())
            {
                model.BlogList.Add(EntityMapperFactory.GetViewBlogMapper().Map(blog));
            }
            return model;
        }

        protected override Blog OnMapBack(ViewBlogListModel model)
        {
            throw new NotImplementedException();
        }
    }
}