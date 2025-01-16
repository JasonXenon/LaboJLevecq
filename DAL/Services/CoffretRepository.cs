using DAL.Entities;
using DAL.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL.Services
{
    public class CoffretRepository : ICoffretRepository
    {
        private readonly string _connectionString;

        public CoffretRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        public void Create(Coffret coffret)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Coffret (Titre, Bonus, Prix, Quantite, Synopsis, GenreId, AfficheUrl) " +
                                      "VALUES (@titre, @bonus, @prix, @quantite, @synopsis, @genreId, @affiche)";

                    cmd.Parameters.AddWithValue("titre", coffret.Titre);
                    cmd.Parameters.AddWithValue("bonus", coffret.Bonus ?? (object)DBNull.Value); // Permet de gérer les valeurs nulles
                    cmd.Parameters.AddWithValue("prix", coffret.Prix);
                    cmd.Parameters.AddWithValue("quantite", coffret.Quantite);
                    cmd.Parameters.AddWithValue("synopsis", coffret.Synopsis ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("genreId", coffret.GenreId);
                    cmd.Parameters.AddWithValue("affiche", coffret.AfficheUrl);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Coffret WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", id);

                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }

                    connection.Close();
                }
            }
        }

        public IEnumerable<Coffret> GetAll()
        {
            List<Coffret> coffrets = new List<Coffret>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Coffret";
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coffrets.Add(new Coffret
                            {
                                Id = (int)reader["Id"],
                                Titre = (string)reader["Titre"],
                                Bonus = (string)reader["Bonus"],
                                Prix = (decimal)reader["Prix"],
                                Quantite = (int)reader["Quantite"],
                                Synopsis = (string)reader["Synopsis"],
                                GenreId = (int)reader["GenreId"],
                                AfficheUrl = (string)reader["AfficheUrl"]
                            });
                        }
                    }
                    connection.Close();
                }
            }
            return coffrets;
        }

        public IEnumerable<Coffret> GetAvailableCoffrets()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coffret> GetByGenreId(int genreId)
        {
            throw new NotImplementedException();
        }

        public Coffret GetById(int id)
        {
            Coffret coffret = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT c.Id, c.Titre, c.Quantite, c.Prix, c.Synopsis, c.GenreId, c.AfficheUrl, g.Label AS GenreLabel
                FROM Coffret c
                INNER JOIN Genre g ON c.GenreId = g.Id
                WHERE c.Id = @id";

                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            coffret = new Coffret
                            {
                                Id = (int)reader["Id"],
                                Titre = (string)reader["Titre"],
                                Quantite = (int)reader["Quantite"],  // Vérifiez que cette ligne récupère la bonne valeur
                                Prix = (decimal)reader["Prix"],
                                Synopsis = (string)reader["Synopsis"],
                                GenreId = (int)reader["GenreId"],
                                AfficheUrl = (string)reader["AfficheUrl"],
                                Genre = new Genre
                                {
                                    Id = (int)reader["GenreId"],
                                    Label = reader["GenreLabel"] as string
                                }
                            };

                            // Vérification de la valeur de Quantite
                            Console.WriteLine($"Quantité récupérée dans GetById pour {coffret.Titre}: {coffret.Quantite}");
                        }
                    }
                }
            }

            if (coffret == null)
            {
                throw new KeyNotFoundException($"Le coffret avec l'ID {id} n'a pas été trouvé.");
            }

            return coffret;
        }




        public void Update(Coffret coffret)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Coffret SET  Quantite = @quantite " +
                        "WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", coffret.Id);
                    cmd.Parameters.AddWithValue("Quantite", coffret.Quantite);

                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }

                    connection.Close();
                }
            }
        }

        // Méthodes GetById, Update, Delete, GetByGenreId et GetAvailableCoffrets suivent une logique similaire.

    }
}
