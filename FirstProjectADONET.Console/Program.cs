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
            string dbName = "University";

            Table table = CreateDatabase;
            table(dbName);

            table = CreateTableGruppa;
            table(dbName);
        }

        #region Создание базы данных
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
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }
            }
        }
        #endregion

        #region Создание таблицы
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

                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }
            }
        }
        #endregion
    }
}