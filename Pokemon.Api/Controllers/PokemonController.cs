using Microsoft.AspNetCore.Mvc;
using Pokemon.Services;

namespace Pokemon.Api.Controllers;
[Route("Pokemons")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;
    public PokemonController(IPokemonService pokemonService) => this._pokemonService = pokemonService;

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Models.Pokemon>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAll()
    {
        var response = await this._pokemonService.GetPokemons();

        if (response is null)
            return NotFound();

        return Ok(response);
    }
}
