using DbUp;
using System;
using System.Configuration;
using System.Reflection;

namespace AirLiquide.DataMigrations
{
    class Program
    {
        static int Main(string[] args)
        {
            var connString = ConfigurationManager.ConnectionStrings["AirLiquide"].ConnectionString;
            EnsureDatabase.For.SqlDatabase(connString);
            
                var runner = DeployChanges.To
                .SqlDatabase(connString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = runner.PerformUpgrade();

            if (result.Successful)
            {
                Console.WriteLine("Scripts deployed");
                return 0;
            }
            Console.WriteLine(result.Error);
            return -1;
        }
    }
}
