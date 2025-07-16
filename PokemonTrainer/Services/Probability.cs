using Microsoft.Extensions.Logging;
using PokemonTrainer.Models;

namespace PokemonTrainer.Services;

public class Probability(ILogger<Probability> logger)
{
    private readonly ILogger<Probability> _logger = logger;
    int timeDelay = 3000;

    public async Task<bool> CalculateCatchChance(Pokemon pokemon)
    {
        // Calculate the catch chance based on the total stats of the Pokemon
        int pokeStatTotal = pokemon.Stats.Sum(stat => stat.BaseStat);
        float catchChance = (1125 - pokeStatTotal) / 10f; // percent is a little high, test and if catching good ones too often set a number like 562 (half of 1125). If under keep chance, if over /2
        
        int attemptsLeft = 3;
        _logger.LogInformation($"You have a {catchChance}% chance to catch them - attepmts left = {attemptsLeft}");

        await Task.Delay(timeDelay);

        while (attemptsLeft > 0)
        {
            int randomChance = new Random().Next(1, 101);
            _logger.LogInformation($"Attempting to catch {pokemon.Name}");
            await Task.Delay(timeDelay);
            if (randomChance <= catchChance)
            {
                _logger.LogInformation($"You caught {pokemon.Name}!");
                return true;
            }
            else
            {
                attemptsLeft--;
                _logger.LogInformation($"Failed to catch {pokemon.Name}. Attempts left: {attemptsLeft}");
                await Task.Delay(timeDelay);
                if (attemptsLeft == 0)
                {
                    _logger.LogInformation($"You failed to catch {pokemon.Name}. It has escaped!");
                    pokemon.Height = null; // Set properties to null for uncaught Pokemon
                    pokemon.Weight = null;
                    pokemon.Types = null;

                    return false;
                }
            }
        }
        return false;
    }
}
