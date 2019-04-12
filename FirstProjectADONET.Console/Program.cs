using System;
using System.Configuration;
using System.Data.Common;

namespace FirstProjectADONET.Console
{
    class Program
    {
        delegate void Table(string text);
        static void Main(string[] args)
        {
            string dbName = "UniversityWS";

            Table table = (text) => CreateDatabase(text);
            table(dbName);

            Table table2 = (text) => CreateTableGruppa(text);
            table2(dbName);
        }
        static void CreateDatabase(string dbName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);

            using (var connection = providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    
                    command.CommandText = $"Create Database {dbName}";
                    //if(command.ExecuteNonQuery() > 0)
                    //    System.Console.WriteLine("База данных создана");
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }
            }
        }
        static void CreateTableGruppa(string dbName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(providerName);

            using (var connection = providerFactory.CreateConnection())
                using(var command = connection.CreateCommand())
            {
                try
                {
                    connectionString = connectionString.Replace("master", dbName);
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.CommandText = "Create Table Gruppa" +
                        "(" +
                        "Id int not null identity primary key," +
                        "[Name] nvarchar(20) not null" +
                        ")";

                    //if (command.ExecuteNonQuery() > 0)
                    //    System.Console.WriteLine("Таблица создана");
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }
            }
        }
    }
}