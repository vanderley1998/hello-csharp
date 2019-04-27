using Microsoft.Extensions.Configuration;
using Plans.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Plans.Api
{
    abstract public class ConnectionDB
    {
        private static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        private static IConfigurationRoot configuration = builder.Build();

        public static PlanModuleDB PlansModule => new PlanModuleDB(configuration.GetConnectionString("PlansModule"));
    }
}
