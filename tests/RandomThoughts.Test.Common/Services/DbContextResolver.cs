using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain;

namespace RandomThoughts.Test.Common.Services
{
    /// <summary>
    ///     Contains the functionalities to
    ///     resolve the DbContext to use.
    /// </summary>
    public class DbContextResolver : IDisposable
    {
        public DbContextResolver()
        {
            Disposed = false;
            ConnectionString =
                $"Server=DESKTOP-FDH5115\\MSSQLSERVER12;Database=Test_{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        /// <summary>
        ///     Gets the <see cref="DbContextOptions"/> object
        ///     used to set up the created context.
        /// </summary>
        public DbContextOptions DbContextOptions { get; private set; }

        /// <summary>
        ///     The default provider to use in the UnitTests
        /// </summary>
        public DbContextProvider ProviderToUse = DbContextProvider.SqliteInMemory;

        /// <summary>
        ///     The Db provider to be used when handling data in the Unit Tests
        /// </summary>
        public enum DbContextProvider
        {
            /// <summary>
            ///     Use this provider for running the tests againts SQLServer
            /// </summary>
            SqlServer,

            /// <summary>
            ///     Use this provider for running the tests with Sqlite in memory
            /// </summary>
            SqliteInMemory,
        }

      
        /// <summary>
        ///     Gets the <see cref="RandomThoughtsDbContext"/> for handling the data.
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     Gets a root of a <see cref="IConfigurationRoot"/> hierarchy.
        /// </summary>
        public IConfigurationRoot Configuration { get; private set; }

        public bool Disposed { get; set; }

        public string ConnectionString { get; set; }


        /// <summary>
        ///     Using the default provider of the one given in
        ///     <paramref name="provider"/>, create the <see cref="RandomThoughtsDbContext"/>
        /// </summary>
        /// <param name="provider">The provider to use during the tests</param>
        /// <returns>An instance of <see cref="RandomThoughtsDbContext"/></returns>
        public DbContext SetContext(DbContextProvider provider = DbContextProvider.SqliteInMemory)
        {
            ProviderToUse = provider;
            if (provider == DbContextProvider.SqliteInMemory)
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                DbContextOptions = new DbContextOptionsBuilder<RandomThoughtsDbContext>()
                    .UseSqlite(connection).Options;
                Context = new RandomThoughtsDbContext((DbContextOptions<RandomThoughtsDbContext>)DbContextOptions);
                connection.Open();
                Context.Database.EnsureCreated();
                Context.Database.ExecuteSqlCommand("ALTER TABLE ThoughtHoles ADD CreatedAt datetime");


            }
            else if (provider == DbContextProvider.SqlServer)
            {
                InitializeConfiguration();

                // Current Assembly where lives DBContexts


                DbContextOptions = new DbContextOptionsBuilder<RandomThoughtsDbContext>()
                    .UseSqlServer(ConnectionString).Options;
                Context = new RandomThoughtsDbContext((DbContextOptions<RandomThoughtsDbContext>) DbContextOptions);
                Context.Database.Migrate();
            }
            

            // I'm not really sure about this, but in SqlServer scenarios
            // We need to drop the database and create it again
            // Context.Database.EnsureDeleted();
            return Context;
        }

        /// <summary>
        ///     Initialize a <see cref="IConfigurationRoot"/> with keys and values
        ///     from the configuration json file
        /// </summary>
        public void InitializeConfiguration()
        {
            // TODO: This is super hardcoded, we need a better solution for get 'appsettings.json' in
            var rootPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("test"));
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(rootPath, "tests", "RandomThoughts.Test.Common")))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            this.Configuration = builder.Build();
        }

        /// <summary>
        ///     Release the external resources.
        /// </summary>
        public void Dispose(DbContextProvider provider = DbContextProvider.SqliteInMemory)
        {
            Context = SetContext(provider);
            //Context.Database.ExecuteSqlCommand(@"EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?';
            //    EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
            //    EXEC sp_MSForEachTable 'DELETE FROM ?';
            //    EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL';
            //    EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?';");
            //Disposed = true;
            Disposed = true;
            //Context?.Dispose();
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        public void Dispose()
        {
            Disposed = true;
            Context.Database.EnsureDeleted();
            Context?.Dispose();

        }

        ~DbContextResolver()
        {
            if (Disposed)
            {
                return;
            }

            Context.Database.EnsureDeleted();
            Context?.Dispose();
        }
    }

    
}
