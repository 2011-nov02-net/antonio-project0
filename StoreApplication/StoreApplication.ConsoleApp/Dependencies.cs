using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using StoreApplication.DataAccess.Entities;
using StoreApplication.Library.Interfaces;
using StoreApplication.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StoreApplication.ConsoleApp
{
    // this class follows the disposable pattern (the standard way to implement the IDisposable interface),
    // so that it can in turn dispose of the contexts it has created.
    public class Dependencies : IDesignTimeDbContextFactory<StoreContext>, IDisposable
    {
        private bool _disposedValue;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        /// <summary>
        /// Set up our reference to the database
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public StoreContext CreateDbContext(string[] args = null)
        {
            var logStream = new StreamWriter("logs.txt");

            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            optionsBuilder.LogTo(logStream.WriteLine);

            return new StoreContext(optionsBuilder.Options);

        }

        /// <summary>
        /// setup the reference to the repository
        /// </summary>
        /// <returns></returns>
        public IStoreRepository CreateStoreRepository()
        {
            var dbContext = CreateDbContext();
            _disposables.Add(dbContext);
            return new StoreRepository(dbContext);
        }

        /// <summary>
        /// Go to to the relative path and get the string
        /// </summary>
        /// <returns></returns>
        static string GetConnectionString()
        {
            string path = "../../../../connection_string.json";
            string json;
            try
            {
                json = File.ReadAllText(path);
            }
            catch (IOException)
            {
                Console.WriteLine($"Required file {path} not found. Should just be the connection string in quotes.");
                throw;
            }
            string connectionString = JsonSerializer.Deserialize<string>(json);
            return connectionString;
        }

        /// <summary>
        /// make sure all opened references are disposed
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    foreach (IDisposable disposable in _disposables)
                    {
                        disposable.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
