using Bloghost.Logic.Configurations;
using Bloghost.Logic.Exceptions;
using Bloghost.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bloghost.Data.Sql;

namespace Bloghost.Logic.Components
{
    internal static class DaoFactoryProvider
    {
        private static Lazy<IDaoFactory> _daoFactory;

        static DaoFactoryProvider()
        {
            _daoFactory = new Lazy<IDaoFactory>(() => CreateDaoFactory());
        }

        public static IDaoFactory GetDaoFactory()
        {
            return _daoFactory.Value;
        }

        private static IDaoFactory CreateDaoFactory()
        {
            //var daoFactorySection = ConfigurationManager.GetSection("daoFactory") as DaoFactorySection;

            //if (daoFactorySection == null)
            //{
            //    throw new ServiceException("daoFactory configuration section couldn't be found");
            //}
            //try
            //{
            //    var daoFactoryType = Type.GetType(daoFactorySection.Type, true);
            //    return (IDaoFactory)Activator.CreateInstance(daoFactoryType);
            //}
            //catch (Exception ex)
            //{
            //    throw new ServiceException("DaoFactory couldn't be instantiated. See inner exception", ex);
            //}
            return new SqlDaoFactory();
        }
    }
}
