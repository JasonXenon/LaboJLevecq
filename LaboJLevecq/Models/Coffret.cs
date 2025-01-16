namespace LaboJLevecq.Models
{
    public class Coffret
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public string Synopsis { get; set; }
        public string Genre { get; set; }
        public int GenreId { get; set; }
        public string AfficheUrl { get; set; }

        public bool IsAvailable()
        {
            return Quantite > 0;
        }
    }
}
