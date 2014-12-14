using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Utilies;

namespace Bloghost.UI.Web.Infrastructure
{
    public static class FileLoader
    {
        public static string ImageUpload(HttpPostedFileBase image, string fileName = null)
        {
            Expect.ArgumentNotNull(image, "image");

            var imagePath = !string.IsNullOrWhiteSpace(fileName) ? Path.GetFileName(fileName + image.FileName.Substring(image.FileName.LastIndexOf('.'))) : Path.GetFileName(image.FileName);
            var localServerPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), imagePath);
            image.SaveAs(localServerPath);
            return imagePath;
        }

    }
}