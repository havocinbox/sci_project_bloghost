using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloghost.Logic.Configurations
{
    public class DaoFactorySection : ConfigurationSection
    {
        public static class PropertyNameFor
        {
            public const string Type = "type";
        }

        [ConfigurationProperty(PropertyNameFor.Type)]
        public string Type
        {
            get { return (string)this[PropertyNameFor.Type]; }
            set { this[PropertyNameFor.Type] = value; }
        }
    }
}
