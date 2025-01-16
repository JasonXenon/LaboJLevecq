using Dal = DAL.Entities;
using Asp = LaboJLevecq.Models;

namespace LaboJLevecq.Tools
{
    public static class CoffretMappers
    {
        public static Dal.Coffret ToDal(this Asp.CoffretForm coffretForm)
        {
            return new Dal.Coffret
            {
                Titre = coffretForm.Titre,
                Bonus = coffretForm.Bonus, // Assuming Bonus is a part of CoffretForm
                Prix = coffretForm.Prix,
                Quantite = coffretForm.Quantite, // Assure the naming matches the Coffret model
                Synopsis = coffretForm.Synopsis,
                GenreId = coffretForm.GenreId,
                AfficheUrl = coffretForm.AfficheUrl
            };
        }

        public static Asp.Coffret ToASP(this Dal.Coffret coffret)
        {
            return new Asp.Coffret
            {
                Id = coffret.Id,
                Titre = coffret.Titre,
                Prix = coffret.Prix,
                Quantite = coffret.Quantite,  // Ajout du mappage de la propriété Quantite
                Genre = coffret.Genre?.Label, // Assuming Genre is a navigation property
                AfficheUrl = coffret.AfficheUrl,
                Synopsis = coffret.Synopsis
            };
        }


        public static Dal.Coffret ToDal(this Asp.Coffret coffret)
        {
            return new Dal.Coffret
            {
                Id = coffret.Id,
                Titre = coffret.Titre,
                Prix = coffret.Prix,
                Quantite = coffret.Quantite,
                GenreId = coffret.GenreId
            };
        }

        public static Dal.Genre ToDal(this Asp.Genre GenreForm)
        {
            return new Dal.Genre
            {
                Label = GenreForm.Nom
            };
        }

        public static Dal.Client ToDal(this Asp.Client clientForm)
        {
            return new Dal.Client
            {
                Nom = clientForm.Nom,
                Prenom = clientForm.Prenom,
                AdresseLivraison = clientForm.AdresseLivraison,
                Email = clientForm.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(clientForm.Password) // Hashage du mot de passe
            };
        }
    }
}
