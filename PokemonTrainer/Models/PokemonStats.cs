using System.Text.Json.Serialization;

namespace PokemonTrainer.Models;

public record PokemonStats
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }

    [JsonPropertyName("stat")]
    public Stat Stat { get; set; }
}

public record Stat
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
