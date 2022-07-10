using System;
using System.Data.Entity;
using System.Configuration;
using ToDoListCommon.Repository;
using System.Data.SqlClient;
using ToDoListCommon.Misc;

namespace ToDoListDbInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            ToDoListDbContext context;

            Console.WriteLine("Wellcome to ToDoListDb initializer.");
            Console.WriteLine("This program will create and configure your DB.");
            Console.WriteLine("This program assumes that you have active SQLEXPRESS instance running!\n");
            Console.WriteLine("Press any button to proceed and generate initial DB...");
            Console.WriteLine("= WARNING = This action will destroy previous DB!\n");
            
            Console.ReadKey();
            Console.WriteLine("Generating database...");
            try
            {
                context = GenerateDB();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while trying to initialize DB.");
                Console.WriteLine("Error message:");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nNow the program will create user accounts.");
            Console.WriteLine("There are two types of users: Administrator and Worker.");

            string adminUsername = "Administrator";
            string adminPassword = GetPassword(adminUsername);
            string workerUsername = "Worker";
            string workerPassword = GetPassword(workerUsername);

            Console.WriteLine($"\nCreating users '{adminUsername}' and '{workerUsername}'...");
            try
            {
                CreateUsers(context, adminUsername, adminPassword, workerUsername, workerPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while trying to create users.");
                Console.WriteLine("Error message:");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Now you're finished setting up a database for use.");
            Console.WriteLine("You may now use a main application.");
            Console.WriteLine("Press any button to exit...");
            Console.ReadKey();
        }

        private static ToDoListDbContext GenerateDB()
        {
            string connStr = ConfigurationManager.ConnectionStrings["conn_str"].ConnectionString;
            ToDoListDbContext context = new ToDoListDbContext(connStr, true);
            context.Projects.Load();
            return context;
        }

        private static string GetPassword(string username)
        {
            string pass;

            do
            {
                Console.Write($"Enter password for '{username}': ");
                pass = Console.ReadLine();
                if (!InputValidator.IsValidPassword(pass))
                {
                    Console.WriteLine("Password should contain only alphanumerical characters");
                    continue;
                }

                Console.Write("Repeat password: ");
                if (Console.ReadLine() != pass)
                {
                    Console.WriteLine("Passwords should match!\n");
                    continue;
                }

                break;

            } while (true);

            return pass;
        }

        private static void CreateUsers(ToDoListDbContext context, string adminName, string adminPass, string workerName, string workerPass)
        {
            try
            {
                context.Database.ExecuteSqlCommand(
                    "USE [ToDoListDb]; " +
                    $"DROP LOGIN [{adminName}]; " +
                    $"DROP LOGIN [{workerName}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            context.Database.ExecuteSqlCommand(
                "USE [ToDoListDb]; " +
                $"CREATE LOGIN [{adminName}] WITH PASSWORD = '{adminPass}'; " +
                $"CREATE LOGIN [{workerName}] WITH PASSWORD = '{workerPass}'");
            context.Database.ExecuteSqlCommand(
                "USE [ToDoListDb]; " +
                $"CREATE USER ADMIN FOR LOGIN [{adminName}]; " +
                $"CREATE USER WORKER FOR LOGIN [{workerName}]");
            context.Database.ExecuteSqlCommand(
                "USE [ToDoListDb]; " +
                "GRANT SELECT, INSERT, DELETE, UPDATE ON SCHEMA :: [dbo] TO ADMIN");
            context.Database.ExecuteSqlCommand(
                "USE [ToDoListDb]; " +
                "GRANT SELECT ON [Projects] TO WORKER; " +
                "GRANT SELECT, INSERT, DELETE, UPDATE ON [dbo].[ToDoes] TO WORKER;" +
                "GRANT SELECT, INSERT, DELETE, UPDATE ON [dbo].[Priorities] TO WORKER;" +
                "GRANT SELECT, INSERT, DELETE, UPDATE ON [dbo].[Tags] TO WORKER;" +
                "GRANT SELECT, INSERT, DELETE, UPDATE ON [dbo].[ToDo_Tag] TO WORKER;");
        }
    }
}
