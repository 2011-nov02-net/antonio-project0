using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using StoreApplication.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreApplication.ConsoleApp
{
    public class Dependencies : IDesignTimeDbContextFactory<StoreContext>, IDisposable
    {
        private bool _disposedValue;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public StoreContext CreateDbContext(string[] args = null)
        {
            using var logStream = new StreamWriter("logs.txt");

            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());

            optionsBuilder.LogTo(logStream.WriteLine, LogLevel.Information);
            return new StoreContext(optionsBuilder.Options);

        }

        static string GetConnectionString()
        {
            string path = "../../../../../../../connection_string.json";
            string json;
            try
            {
                json = File.ReadAllText(path);
            }
            catch (IOException)
            {
                Console.WriteLine($"required file {path} not found. should just be the connection string in quotes.");
                throw;
            }
            string connectionString = JsonSerializer.Deserialize<string>(json);
            return connectionString;
        }
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
