using System.Text.Json.Serialization;

namespace Testando_Json.Entities
{
    class Music
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("song")]
        public string Song { get; set; }

        [JsonPropertyName("genre")]
        public string Genre { get; set; }
        public Music(string artist, string song, string genre)
        {
            Artist = artist;
            Song = song;
            Genre = genre;
        }

        public Music()
        {
        }

        public void printGenre(IEnumerable<string> genres)
        {
            Console.WriteLine("Lista de Gêneros:");
            genres.ToList().ForEach(genre => Console.WriteLine($"- {genre}"));
        }
        public void printArtist(IEnumerable<string> artists)
        {
            Console.WriteLine("Lista de Artistas: ");
            artists.ToList().ForEach(artists => Console.WriteLine($"- {artists}"));
        }
        public void printFilteredGenre(IEnumerable<IGrouping<string, Music>> groupedGenre)
        {
            groupedGenre.SelectMany(group => group.Select(music => $"{music.Artist}").OrderBy(m => m)).Distinct().ToList().ForEach(result => Console.WriteLine(result));
        }
        public void printFilteredArtist(IEnumerable<IGrouping<string, Music>> filterMusic)
        {
            filterMusic.SelectMany(group => group.Select(music => $"{music.Song}").OrderBy(m => m)).Distinct().ToList().ForEach(result => Console.WriteLine(result));
        }

    }
}