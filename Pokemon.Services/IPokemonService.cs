using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Services
{
    public interface IPokemonService
    {
        Task<IEnumerable<Models.Pokemon>> GetPokemons();
    }
}
