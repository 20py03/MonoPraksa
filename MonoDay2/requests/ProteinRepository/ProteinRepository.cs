using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProteinRepository
{
    internal class ProteinRepository
    {

        /*
        private readonly string _connectionString;

        public ProteinRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HttpResponseMessage AddNewProtein(CreateProtein protein)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                Guid id = Guid.NewGuid();
                string commandText = $"INSERT INTO \"Protein\" (\"Id\",\"Flavor\",\"Price\",\"Weight\",\"CategoryId\") VALUES (@id,@flavor,@price,@weight,@categoryId)";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("flavor", protein.Flavor);
                    command.Parameters.AddWithValue("price", protein.Price);
                    command.Parameters.AddWithValue("weight", protein.Weight);
                    command.Parameters.AddWithValue("categoryId", protein.CategoryId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent($"Added a new protein {protein.Flavor}")
                        };
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                            Content = new StringContent($"Failed to add a new protein {protein.Flavor}")
                        };
                    }
                }
            }
        }
        */
    }
}
