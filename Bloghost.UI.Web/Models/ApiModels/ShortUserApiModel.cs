using System;

namespace Bloghost.UI.Web.Models.ApiModels
{
    public class ShortUserApiModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RoleName { get; set; }
    }
}