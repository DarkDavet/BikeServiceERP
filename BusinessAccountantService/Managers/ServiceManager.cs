using BusinessAccountantService.Data;
using BusinessAccountantService.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccountantService.Managers
{
    public class ServiceManager
    {
        public List<ServiceItem> GetAllServices()
        {
            var services = new List<ServiceItem>();
            using (var connection = new SqliteConnection(DatabaseService.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ServiceName, DefaultPrice FROM ServicePriceList";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(new ServiceItem
                        {
                            Name = reader.GetString(0),
                            Price = reader.GetDecimal(1)
                        });
                    }
                }
            }
            return services;
        }

        public void AddService(string name, decimal price)
        {
            using (var connection = new SqliteConnection(DatabaseService.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT OR REPLACE INTO ServicePriceList (ServiceName, DefaultPrice) VALUES ($name, $price)";
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$price", price);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateService(string oldName, string newName, decimal newPrice)
        {
            using (var connection = new SqliteConnection(DatabaseService.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE ServicePriceList SET ServiceName = $newName, DefaultPrice = $newPrice WHERE ServiceName = $oldName";
                command.Parameters.AddWithValue("$oldName", oldName);
                command.Parameters.AddWithValue("$newName", newName);
                command.Parameters.AddWithValue("$newPrice", newPrice);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteService(string name)
        {
            using (var connection = new SqliteConnection(DatabaseService.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM ServicePriceList WHERE ServiceName = $name";
                command.Parameters.AddWithValue("$name", name);
                command.ExecuteNonQuery();
            }
        }
    }
}