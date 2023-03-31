namespace Individual_Project___Eren_Destan.Models
{
    public class Vinyl : Product
    {
        private int vinylType;
        private string artist;
        private Genre genre;
        private DateTime releaseDate;

        //public Vinyl() { }
        public Vinyl(int id, string name, int vinylType, string artist, Genre genre, DateTime releaseDate, double price, int stock) : base(id, name, price, stock)
        {
            this.name= name;
            this.vinylType = vinylType;
            this.artist = artist;
            this.genre = genre;
            this.releaseDate = releaseDate;
        }
        public int VinylType { get; set; }
        public string Artist { get; set; } 
        public Genre Genre { get; set; } 
        public DateTime ReleaseDate { get; set; }
    }
}
