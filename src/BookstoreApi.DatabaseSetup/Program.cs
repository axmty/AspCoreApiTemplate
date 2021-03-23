using System;
using DbUp;

namespace BookstoreApi.DatabaseSetup
{
    public static class Program
    {
        public static int Main()
        {
            var connectionString = @"Server=DESKTOP-C5T4GM0\SQLEXPRESS;Database=BOOKSTORE;Trusted_Connection=True;";
            
            DropDatabase.For.SqlDatabase(connectionString);
            EnsureDatabase.For.SqlDatabase(connectionString);
            
            var upgrader = DeployChanges
                .To
                .SqlDatabase(connectionString)
                .WithScriptsFromFileSystem("../../../Scripts")
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            return 0;
        }
    }
}
