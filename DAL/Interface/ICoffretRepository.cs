using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICoffretRepository
    {
        /// <summary>
        /// Crée un coffret dans la base de données.
        /// </summary>
        /// <param name="coffret">Le coffret à créer.</param>
        void Create(Coffret coffret);

        /// <summary>
        /// Récupère tous les coffrets.
        /// </summary>
        /// <returns>Une liste de coffrets.</returns>
        IEnumerable<Coffret> GetAll();

        /// <summary>
        /// Récupère un coffret par son ID.
        /// </summary>
        /// <param name="id">L'ID du coffret.</param>
        /// <returns>Le coffret correspondant.</returns>
        Coffret GetById(int id);

        /// <summary>
        /// Met à jour un coffret.
        /// </summary>
        /// <param name="coffret">Le coffret à mettre à jour.</param>
        void Update(Coffret coffret);

        /// <summary>
        /// Supprime un coffret par son ID.
        /// </summary>
        /// <param name="id">L'ID du coffret à supprimer.</param>
        void Delete(int id);

        /// <summary>
        /// Récupère les coffrets par genre.
        /// </summary>
        /// <param name="genreId">L'ID du genre.</param>
        /// <returns>Une liste de coffrets correspondant au genre.</returns>
        IEnumerable<Coffret> GetByGenreId(int genreId);

        /// <summary>
        /// Récupère les coffrets disponibles (stock > 0).
        /// </summary>
        /// <returns>Une liste de coffrets disponibles.</returns>
        IEnumerable<Coffret> GetAvailableCoffrets();
    }
}
