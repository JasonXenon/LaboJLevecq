using DAL.Entities;
using DAL.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class GenreRepository : IGenreRepository
    {
        private readonly string _connectionString;

        public GenreRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        public void Create(Genre genre)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Genre (Label) VALUES (@titre)";
                    cmd.Parameters.AddWithValue("titre", genre.Label);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public IEnumerable<Genre> GetAll()
        {
            List<Genre> genres = new List<Genre>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Genre";

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genres.Add(new Genre
                            {
                                Id = (int)reader["Id"],
                                Label = (string)reader["Label"]
                            });
                        }
                    }

                    connection.Close();
                }
            }

            return genres;
        }
    }
}
