namespace LaboJLevecq.Models
{
    public class CoffretForm
    {
        public string Titre { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public string Synopsis { get; set; }
        public int GenreId { get; set; }  // Cette propriété permet de lier le genre sélectionné
        public string AfficheUrl { get; set; }
        public string Bonus { get; set; }

        // Ajout d'une liste de genres pour l'affichage dans la vue
        public List<GenreListItem> Genres { get; set; } = new List<GenreListItem>();
    }
}
