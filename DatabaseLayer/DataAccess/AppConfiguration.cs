using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatabaseLayer.DataAccess
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            var appsettings = root.GetSection("MyConnectionString");
            sqlConnectionString = appsettings.Value;
        }

        public string sqlConnectionString { get; set; }
    }
}
