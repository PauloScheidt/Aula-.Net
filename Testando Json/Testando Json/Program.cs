using System.Text.Json;
using Testando_Json.Entities;

using (HttpClient client = new HttpClient())
{

    try
    {

        string resposta = await client.GetStringAsync("https://guilhermeonrails.github.io/api-csharp-songs/songs.json");
        var musicas = JsonSerializer.Deserialize<List<Music>>(resposta)!;
        bool continuar = true;

        while (continuar)
        {

            Console.WriteLine("Qual funcionalidade deseja escolher? ");
            Console.WriteLine("Exibir todos os gêneros musicais (1)");
            Console.WriteLine("Ordenar artistas por nome (2)");
            Console.WriteLine("Filtrar artistas por gênero musical (3)");
            Console.WriteLine("Filtrar músicas de um artista (4)");
            Console.WriteLine("Deseja sair? (0)");
            int resp = int.Parse(Console.ReadLine());

            switch (resp)
            {

                case 1:
                    Console.WriteLine("Opção 1 selecionada.");
                    var showGenre = musicas.OrderBy(m => m.Genre).Select(m => m.Genre.Split(',')[0].ToUpper()).Distinct();
                    new Music().printGenre(showGenre);
                    break;

                case 2:
                    Console.WriteLine("Opção 2 selecionada.");
                    var orderedArtists = musicas.OrderBy(m => m.Artist).Select(m => m.Artist).Distinct();
                    new Music().printArtist(orderedArtists);
                    break;

                case 3:
                    Console.WriteLine("Opção 3 selecionada.");
                    while (true)
                    {
                        Console.WriteLine("Qual gênero deseja filtar: ");
                        string choseGenre = Console.ReadLine();
                        var filteredMusicas = musicas.Where(m => m.Genre.Split(',')[0].ToUpper() == choseGenre.ToUpper());

                        if (filteredMusicas.Any())
                        {
                            var groupedGenre = filteredMusicas.GroupBy(m => m.Genre.Split(',')[0].ToUpper());

                            new Music().printFilteredGenre(groupedGenre);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum gênero corresponde a pesquisa!");
                        }
                    }
                    Console.WriteLine();

                    break;

                case 4:
                    Console.WriteLine("Opção 4 selecionada.");
                    while (true)
                    {
                        Console.WriteLine("Qual é o artista que deseja filtrar: ");
                        string choseArtist = Console.ReadLine();
                        var filteredArtists = musicas.Where(m => m.Artist.Contains(choseArtist));

                        if (filteredArtists.Any())
                        {
                            var filterMusic = filteredArtists.OrderBy(m => m.Artist).GroupBy(m => m.Artist);
                           
                            new Music().printFilteredArtist(filterMusic);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nenhum artista corresponde a pesquisa! Tente novamente.");
                        }
                    }
                    Console.WriteLine();
                    break;

                case 0:
                    continuar = false;
                    Console.WriteLine("Saindo do programa...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    catch (HttpRequestException)
    {
        Console.WriteLine("Error downloading data...");
    }

    catch (Exception)
    {
        Console.WriteLine("Ocorreu um erro inesperado!");
    }
}