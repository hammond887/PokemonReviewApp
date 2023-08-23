using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonRepository pokemonRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        { 
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200,Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            { 
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId,[FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
            {
                return BadRequest(ModelState);
            }

            var pokemon = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            if (!_pokemonRepository.CreatePokemon(ownerId,catId,pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while suving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int pokemonId, [FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto updatePokemon)
        {
            if (updatePokemon == null)
            {
                return BadRequest(ModelState);
            }

            if (pokemonId != updatePokemon.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pokemonMap = _mapper.Map<Pokemon>(updatePokemon);

            if (!_pokemonRepository.UpdatePokemon(ownerId,catId,pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong updating pokemon");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokemonId)
        {
            if (!_pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var pokemonToDelete = _pokemonRepository.GetPokemon(pokemonId);
            var reviewsToDelete = _reviewRepository.GetReviewsOfAPokemon(pokemonId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Somewthing went wrong deleting reviews.");
            }

            if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting pokemon");
            }

            return NoContent();
        }

    }
}
