using DAL.Entities;
using DAL.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using BCrypt.Net;

namespace DAL.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        public void Create(Client client)
        {

            if (EmailExists(client.Email))
            {
                throw new Exception("Un client avec cet email existe déjà.");
            }
                // Hachage du mot de passe avant de l'ajouter à la base de données
                client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Client (Nom, Prenom, AdresseLivraison, Email, PasswordHash) " +
                                      "VALUES (@nom, @prenom, @adresse, @email, @passwordHash)";
                    cmd.Parameters.AddWithValue("nom", client.Nom);
                    cmd.Parameters.AddWithValue("prenom", client.Prenom);
                    cmd.Parameters.AddWithValue("adresse", client.AdresseLivraison);
                    cmd.Parameters.AddWithValue("email", client.Email);
                    cmd.Parameters.AddWithValue("passwordHash", client.Password);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Client WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(Client client)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Client SET Nom = @nom, Prenom = @prenom, AdresseLivraison = @adresse, " +
                                      "Email = @email, Password = @password WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", client.Id);
                    cmd.Parameters.AddWithValue("nom", client.Nom);
                    cmd.Parameters.AddWithValue("prenom", client.Prenom);
                    cmd.Parameters.AddWithValue("adresse", client.AdresseLivraison);
                    cmd.Parameters.AddWithValue("email", client.Email);
                    cmd.Parameters.AddWithValue("password", client.Password);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public IEnumerable<Client> GetAll()
        {
            List<Client> clients = new List<Client>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Client";
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                Id = (int)reader["Id"],
                                Nom = (string)reader["Nom"],
                                Prenom = (string)reader["Prenom"],
                                AdresseLivraison = (string)reader["AdresseLivraison"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"]
                            });
                        }
                    }
                    connection.Close();
                }
            }
            return clients;
        }

        public Client GetById(int id)
        {
            Client client = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Client WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                Id = (int)reader["Id"],
                                Nom = (string)reader["Nom"],
                                Prenom = (string)reader["Prenom"],
                                AdresseLivraison = (string)reader["AdresseLivraison"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"]
                            };
                        }
                    }
                    connection.Close();
                }
            }
            if (client == null) throw new NullReferenceException("Le client n'existe pas");
            return client;
        }

        public Client GetByEmail(string email)
        {
            Client client = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Client WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("Email", email);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                Id = (int)reader["Id"],
                                Nom = (string)reader["Nom"],
                                Prenom = (string)reader["Prenom"],
                                AdresseLivraison = (string)reader["AdresseLivraison"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"]
                            };
                        }
                    }
                    connection.Close();
                }
            }
            return client;
        }

        // Méthode pour vérifier si un email existe déjà
        private bool EmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(1) FROM Client WHERE Email = @Email";
                    cmd.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    connection.Close();

                    return count > 0; // Si un client avec cet email existe, retourne true
                }
            }
        }
    }
}
