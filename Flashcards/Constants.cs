using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Flashcards
{
    internal static class Constants
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    }
}
