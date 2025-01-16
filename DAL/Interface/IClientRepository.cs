using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IClientRepository
    {
        /// <summary>
        /// Crée un client dans la base de données.
        /// </summary>
        /// <param name="client">Le client à créer.</param>
        void Create(Client client);

        /// <summary>
        /// Récupère tous les clients.
        /// </summary>
        /// <returns>Une liste de clients.</returns>
        IEnumerable<Client> GetAll();

        /// <summary>
        /// Récupère un client par son ID.
        /// </summary>
        /// <param name="id">L'ID du client.</param>
        /// <returns>Le client correspondant.</returns>
        Client GetById(int id);

        /// <summary>
        /// Met à jour un client.
        /// </summary>
        /// <param name="client">Le client à mettre à jour.</param>
        void Update(Client client);

        /// <summary>
        /// Supprime un client par son ID.
        /// </summary>
        /// <param name="id">L'ID du client à supprimer.</param>
        void Delete(int id);

        /// <summary>
        /// Récupère un client par son email.
        /// </summary>
        /// <param name="email">L'email du client.</param>
        /// <returns>Le client correspondant.</returns>
        Client GetByEmail(string email);
    }
}
