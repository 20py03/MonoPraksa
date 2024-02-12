using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using requests.Model;
using Npgsql;
using Repository.Common;

namespace requests.Repository
{
    public class ProteinRepository : IProteinRepository
    {
        private string _connectionString = "Host=localhost;Port=5432;Database=Protein;Username=postgres;Password=postgres;";
            
        public async Task<int> AddNewProteinAsync(Protein protein)
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

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public async Task<List<GetProteinWithCategory>> GetAllProteinsAsync()
        {
            string CommandText = "SELECT p.*, c.\"Vegan\", c.\"Anabolic\", c.\"Recovery\" " +
                                 "FROM \"Protein\" p " +
                                 "JOIN \"Category\" c ON p.\"CategoryId\" = c.\"Id\"";

            List<GetProteinWithCategory> proteinList = new List<GetProteinWithCategory>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(CommandText, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string flavor = reader.GetString(reader.GetOrdinal("Flavor"));
                                double price = reader.GetDouble(reader.GetOrdinal("Price"));
                                int weight = reader.GetInt32(reader.GetOrdinal("Weight"));
                                bool isVegan = reader.GetBoolean(reader.GetOrdinal("Vegan"));
                                bool isAnabolic = reader.GetBoolean(reader.GetOrdinal("Anabolic"));
                                bool isRecovery = reader.GetBoolean(reader.GetOrdinal("Recovery"));

                                proteinList.Add(new GetProteinWithCategory(flavor, price, weight, isVegan, isAnabolic, isRecovery));
                            }
                            return proteinList;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<int> DeleteProteinAsync(Guid id)
        {
            string CommandText = "DELETE FROM \"Protein\" WHERE \"Id\" = @Id";

            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);

            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand(CommandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public async Task<List<Protein>> GetProteinByIdAsync(Guid id)
        {
            string CommandText = "SELECT * FROM \"Protein\" WHERE \"Id\" = @Id";

            List<Protein> proteinViews = new List<Protein>();

            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);

            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand(CommandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string flavor = reader.GetString(reader.GetOrdinal("Flavor"));
                        double price = reader.GetDouble(reader.GetOrdinal("Price"));
                        int weight = reader.GetInt32(reader.GetOrdinal("Weight"));

                        proteinViews.Add(new Protein(flavor, price, weight));
                    }
                    return proteinViews;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<int> PutProteinPriceAsync(Guid id, double price)
        {
            try
            {
                string CommandText = "UPDATE \"Protein\" SET \"Price\" = @NewPrice WHERE \"Id\" = @Id";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(CommandText, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@NewPrice", price);

                        connection.Open();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
