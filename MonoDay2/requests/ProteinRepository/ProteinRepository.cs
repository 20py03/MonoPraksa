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
using requests.SortingPaging.Common;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Numerics;

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

        public async Task<PagedList<GetProteinWithCategory>> GetAllProteinsAsync(Filtering filtering, Sorting sorting, Paging paging)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            StringBuilder commandText = new StringBuilder("SELECT p.*, c.\"Vegan\", c.\"Anabolic\", c.\"Recovery\" " +
                "FROM \"Protein\" p " +
                "JOIN \"Category\" c ON p.\"CategoryId\" = c.\"Id\" " +
                "WHERE 1=1");

            SetFilters(commandText, filtering, command);
            if (!string.IsNullOrEmpty(sorting.SortBy) && paging.PageNumber > 0 && paging.PageSize > 0)
            {
                commandText.Append($" ORDER BY \"{sorting.SortBy}\" {sorting.SortOrder} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
                command.Parameters.AddWithValue("@Offset", (paging.PageNumber - 1) * paging.PageSize);
                command.Parameters.AddWithValue("@PageSize", paging.PageSize);
            }

            List<GetProteinWithCategory> proteinList = new List<GetProteinWithCategory>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))

            using (command)
            {
                command.Connection = connection;
                command.CommandText = commandText.ToString();
                await connection.OpenAsync();

                using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Guid id = reader.GetGuid(reader.GetOrdinal("Id"));
                        string flavorValue = reader.GetString(reader.GetOrdinal("Flavor"));
                        double priceValue = reader.GetDouble(reader.GetOrdinal("Price"));
                        int weightValue = reader.GetInt32(reader.GetOrdinal("Weight"));
                        bool proteinIsVegan = reader.GetBoolean(reader.GetOrdinal("Vegan"));
                        bool proteinIsAnabolic = reader.GetBoolean(reader.GetOrdinal("Anabolic"));
                        bool proteinIsRecovery = reader.GetBoolean(reader.GetOrdinal("Recovery"));

                        proteinList.Add(new GetProteinWithCategory(id,flavorValue, priceValue, weightValue, proteinIsVegan, proteinIsAnabolic, proteinIsRecovery));
                    }
                }
            }
            int totalCount = await GetTotalProteinCount(filtering);
            return new PagedList<GetProteinWithCategory>(proteinList, paging.PageSize, totalCount);
        }

        public void SetFilters(StringBuilder commandText, Filtering filtering, NpgsqlCommand command)
        {
            if (!string.IsNullOrWhiteSpace(filtering.Flavor))
            {
                commandText.Append($" AND p.\"Flavor\" LIKE @Flavor ");
                command.Parameters.AddWithValue("@Flavor", filtering.Flavor);
            }
            if (filtering.MinPrice != null)
            {
                commandText.Append($" AND p.\"Price\" >= @MinPrice");
                command.Parameters.AddWithValue("@MinPrice", filtering.MinPrice);
            }
            if (filtering.MaxPrice != null)
            {
                commandText.Append($" AND p.\"Price\" <= @MaxPrice ");
                command.Parameters.AddWithValue("@MaxPrice", filtering.MaxPrice);
            }
            if (filtering.MinWeight != null)
            {
                commandText.Append($" AND p.\"Weight\" >= @MinWeight");
                command.Parameters.AddWithValue("@MinWeight", filtering.MinWeight);
            }
            if (filtering.MaxWeight != null)
            {
                commandText.Append($" AND p.\"Weight\" <= @MaxWeight ");
                command.Parameters.AddWithValue("@MaxWeight", filtering.MaxWeight);
            }
        }

        private async Task<int> GetTotalProteinCount(Filtering filtering)
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                StringBuilder querryBuilder = new StringBuilder("SELECT COUNT(*) FROM \"Protein\" WHERE 1=1");
                SetFilters(querryBuilder, filtering, command);
                command.CommandText = querryBuilder.ToString();
                connection.Open();
                var result = await command.ExecuteScalarAsync();
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    return count;
                }
                return 0;
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

        public async Task<List<Category>> GetCategoryListAsync()

        {
            string CommandText = "SELECT * FROM \"Category\"";

            List<Category> categoryViews = new List<Category>();

            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);

            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand(CommandText, connection);
                connection.Open();
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Guid id = reader.GetGuid(reader.GetOrdinal("Id"));
                        bool isVegan = reader.GetBoolean(reader.GetOrdinal("Vegan"));
                        bool isAnabolic = reader.GetBoolean(reader.GetOrdinal("Anabolic"));
                        bool isRecovery = reader.GetBoolean(reader.GetOrdinal("Recovery"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));

                        Category category = new Category(id, isVegan,isAnabolic, isRecovery,name);
                        categoryViews.Add(category);
                    }
                    return categoryViews;
                }else
                {
                    return null;
                }
            }
        }

        public async Task<int> AddCategoryNameByIdAsync(Guid id, string categoryName)
        {
            string CommandText = "UPDATE \"Category\" SET \"Name\" = @Name WHERE \"Id\" = @Id";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(CommandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", categoryName);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteProteinAsync(Guid id)
        {

            List<Protein> proteinToDelete = await GetProteinByIdAsync(id);
            if(proteinToDelete.Count == 0)
            {
                return 0;
            }

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


        public async Task<int> PutProteinPriceAsync(Guid id, double price)
        {
            try
            {
                List<Protein> proteinToUpdate = await GetProteinByIdAsync(id);
                if(proteinToUpdate.Count == 0)
                {
                    return 0;
                }

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
