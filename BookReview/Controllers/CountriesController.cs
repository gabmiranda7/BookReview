using BookReview.DTOs;
using BookReview.Interfaces;
using BookReview.Models;
using BookReview.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCountries()
        {
            var countries = _countryRepository.GetCountries();
            var countryDtos = countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(countryDtos);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("País não encontrado");

            var country = _countryRepository.GetCountry(countryId);
            var countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryDto);
        }

        [HttpGet("author/{authorId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByAuthor(int authorId)
        {
            var country = _countryRepository.GetCountryByAuthor(authorId);
            var countryDto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
            };

            if (country == null)
                return NotFound("País não encontrado para este autor.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(countryDto);
        }

        [HttpGet("{countryId}/authors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorsFromCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("País não encontrado");
            var authors = _countryRepository.GetAuthorsFromCountry(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(authors);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto newCountry)
        {
            if (newCountry == null)
                return BadRequest("Dados inválidos.");

            var existingCountry = _countryRepository.GetCountries()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == newCountry.Name.Trim().ToUpper());
            if (existingCountry != null)
            {
                ModelState.AddModelError("", "País já existente.");
                return StatusCode(422, ModelState);
            }

            var countryToCreate = new Country
            {
                Name = newCountry.Name
            };

            if (!_countryRepository.CreateCountry(countryToCreate))
            {
                ModelState.AddModelError("", "Algo de errado aconteceu tentando salvar o país.");
                return StatusCode(500, ModelState);
            }

            return Ok("País criado com sucesso.");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null || countryId != updatedCountry.Id)
                return BadRequest("País inválido.");
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("País não encontrado.");
            var countryToUpdate = _countryRepository.GetCountry(countryId);
            countryToUpdate.Name = updatedCountry.Name;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _countryRepository.UpdateCountry(countryToUpdate);
            return NoContent();
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return BadRequest("País não encontrado");

            var countryToDelete = _countryRepository.GetCountry(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Algo de errado aconteceu tentando excluir o país.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}