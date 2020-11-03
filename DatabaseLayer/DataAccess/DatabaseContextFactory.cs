using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.DataAccess
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfig = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<DataContext>();
            opsBuilder.UseSqlServer(appConfig.sqlConnectionString);
            return new DataContext(opsBuilder.Options);
        }
    }
}
