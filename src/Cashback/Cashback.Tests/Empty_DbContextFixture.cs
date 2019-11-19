using Cashback.Domain;
using Cashback.Domain.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cashback.Tests
{
    public class Empty_DbContextFixture : IDisposable
    {
        public CashbackDbContext DbContext { get; private set; }
        public CashbackCommandsHandler CommandsHandler { get; set; }

        public Empty_DbContextFixture()
        {
            var builder1 = new DbContextOptionsBuilder<CashbackDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .EnableSensitiveDataLogging()
               .ConfigureWarnings(warnings => warnings
                   .Throw(CoreEventId.IncludeIgnoredWarning)
                   .Throw(RelationalEventId.QueryClientEvaluationWarning));
            DbContext = new CashbackDbContext(builder1.Options);

            CommandsHandler = new CashbackCommandsHandler(DbContext);

            CashbackStartup.Configure(null);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }

    [CollectionDefinition("Empty database collection")]
    public class DbContextEmptyCollection : ICollectionFixture<Empty_DbContextFixture>
    {
    }
}
