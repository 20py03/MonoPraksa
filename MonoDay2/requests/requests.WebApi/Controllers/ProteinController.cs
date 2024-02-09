﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using requests.WebApi.Models;
using System.Net.Http;
using System.Net;
using Npgsql;
using System.Web.UI;

namespace requests.WebApi.Controllers
{
    public class ProteinController : ApiController
    {
        //static - postoji samo jedna instanca objekta
        private static List<Protein> proteinList = new List<Protein>();
        private string connectionString = "Host=localhost;Port=5432;Database=Protein;Username=postgres;Password=postgres;";
        NpgsqlConnection connection = new NpgsqlConnection();
        NpgsqlCommand command = new NpgsqlCommand();


        /*
        private readonly ProteinRepository _proteinRepository;

        public ProteinService(ProteinRepository proteinRepository)
        {
            _proteinRepository = proteinRepository;
        }
        // POST : Add new protein
        public HttpResponseMessage AddNewProtein(CreateProtein protein)
        {
            return _proteinRepository.AddNewProtein(protein);
        }
        */

        public HttpResponseMessage PostAddNewProtein(CreateProtein protein)
        {
            if (protein == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please enter some values.");
            }

            connection = new NpgsqlConnection(connectionString);
            using (connection)
            {
                Guid id = Guid.NewGuid();

                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO \"Protein\" (\"Id\",\"Flavor\",\"Price\",\"Weight\",\"CategoryId\") VALUES (@id,@flavor,@price,@weight,@categoryId)";
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("flavor", protein.Flavor);
                command.Parameters.AddWithValue("price", protein.Price);
                command.Parameters.AddWithValue("weight", protein.Weight);
                command.Parameters.AddWithValue("categoryId", protein.CategoryId);

                connection.Open();

                if (command.ExecuteNonQuery() <= 0)
                {
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Failed to add a new protein {protein.Flavor}");
                }

                connection.Close();
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"Added a new protein {protein.Flavor}");
        }


        //GET : Get protein
        public HttpResponseMessage GetAllProteins()
        {
            string CommandText = "SELECT p.*, c.\"Vegan\", c.\"Anabolic\", c.\"Recovery\" " +
                                 "FROM \"Protein\" p " +
                                 "JOIN \"Category\" c ON p.\"CategoryId\" = c.\"Id\"";

            List<GetProteinWithCategory> proteinList = new List<GetProteinWithCategory>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(CommandText, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
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
                            return Request.CreateResponse(HttpStatusCode.OK, proteinList);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No proteins found.");
                        }
                    }
                }
            }
        }



    //GET : Get protein by id
    public HttpResponseMessage GetProteinById(Guid id)
        {
            string CommandText = "SELECT * FROM \"Protein\" WHERE \"Id\" = @Id";

            List<GetProtein> proteinViews = new List<GetProtein>();

            connection = new NpgsqlConnection(connectionString);

            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand(CommandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string flavor = reader.GetString(reader.GetOrdinal("Flavor"));
                        double price = reader.GetDouble(reader.GetOrdinal("Price"));
                        int weight = reader.GetInt32(reader.GetOrdinal("Weight"));

                        proteinViews.Add(new GetProtein(flavor, price, weight));
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, proteinViews);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Protein with ID {id} not found.");
                }
            }
        }


        [HttpPut]
        //PUT : Update protein price
        public HttpResponseMessage PutProteinPrice(Guid id, [FromBody] UpdateProtein protein)
        {
            try
            {
                string CommandText = "UPDATE \"Protein\" SET \"Price\" = @NewPrice WHERE \"Id\" = @Id";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(CommandText, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@NewPrice", protein.Price); 

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, $"Price for protein with ID {id} updated successfully.");
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Protein with ID {id} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error updating price for protein with ID {id}: {ex.Message}");
            }
        }



        //DELETE : Delete protein by id
        public HttpResponseMessage DeleteProteinById(Guid id)
        {
            string CommandText = "DELETE FROM \"Protein\" WHERE \"Id\" = @Id";

            connection = new NpgsqlConnection(connectionString);

            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand(CommandText, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Protein with ID {id} deleted successfully.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Protein with ID {id} not found.");
                }
            }
        }
    }
}
