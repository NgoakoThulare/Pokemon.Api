using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokemon.Models;

namespace Pokemon.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        public PokemonService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;

            //this._appSettings = configuration.GetSection("PokemonSettings").Get<AppSettings>();
        }
        public async Task<IEnumerable<Models.Pokemon>> GetPokemons()
        {
            string tempEndpoint = "https://pokeapi.co/api/v2/pokemon?offset=300&limit=100";
            string result = string.Empty;
            var pokemons = new List<Models.Pokemon>();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, tempEndpoint);
            var client = this._httpClientFactory.CreateClient();
            var response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();

                var deserializedObj = JsonConvert.DeserializeObject<Result>(result);

                pokemons = deserializedObj?.results;

                await GetDetails(pokemons);
            }
            return pokemons;
        }

        private async Task<Details> GetDetails(List<Models.Pokemon> pokemons)
        {
            var details = new Details();
            foreach (var pokemon in pokemons)
            {
                string result = string.Empty;

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, pokemon.url);
                var client = this._httpClientFactory.CreateClient();
                var response = await client.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                    details = JsonConvert.DeserializeObject<Details>(result);

                    pokemon.imageUrl = details?.sprites?.front_default;
                    pokemon.stats = details?.stats;
                }
                details?.pokemon?.Add(pokemon);
            }
            return details;
        }
    }
}
