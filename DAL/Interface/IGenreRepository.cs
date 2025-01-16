using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IGenreRepository
    {
        /// <summary>
        /// Crée un coffret dans la base de données.
        /// </summary>
        /// <param name="coffret">Le genre à été créer.</param>
        void Create(Genre genre);

        IEnumerable<Genre> GetAll();
    }
}
