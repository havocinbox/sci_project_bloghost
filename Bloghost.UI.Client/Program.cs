using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bloghost.UI.Client.Models;

namespace Bloghost.UI.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = new Screen();
            screen.ShowMainMenu();
        }
    }
}
