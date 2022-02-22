using CrimeService.Entities;
using CrimeService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimeService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CrimesController : ControllerBase
    {
        private readonly ILogger<CrimesController> _logger;
        private readonly ICrimesRepository _crimesRepository;

        public CrimesController(ILogger<CrimesController> logger, ICrimesRepository crimesRepository)
        {
            _logger = logger;
            _crimesRepository = crimesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CrimeDto>> GetAsync()
        {
            var crimes = (await _crimesRepository.GetAllAsync()).Select(crime => crime.AsDto());
            return crimes;
        }

        // GET /crimes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CrimeDto>> GetByIdAsync(Guid id)
        {
            var crime = await _crimesRepository.GetAsync(id);

            if (crime == null)
            {
                _logger.LogInformation($"Crime Not Found");
                return NotFound();
            }
            return crime.AsDto();
        }

        // POST /crimes
        [HttpPost]
        public async Task<ActionResult<CrimeDto>> PostAsync(CreateCrimeDto createCrimeDto)
        {
            var crime = new Crime
            {
                EventDate = DateTimeOffset.UtcNow,
                EventType = createCrimeDto.EventType,
                Description = createCrimeDto.Description,
                EventPlace = createCrimeDto.EventPlace,
                Email = createCrimeDto.Email,
                Status = createCrimeDto.Status,
                LawEnforcementId = createCrimeDto.LawEnforcementId
            };

            await _crimesRepository.CreateAsync(crime);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = crime.Id }, crime);
        }

        // PUT /crimes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateCrimeDto updateCrimeDto)
        {
            var existingCrime = await _crimesRepository.GetAsync(id);

            if (existingCrime == null)
            {
                _logger.LogInformation($"Crime with Your Id Not Found");
                return NotFound();
            }

            existingCrime.EventType = updateCrimeDto.EventType;
            existingCrime.Description = updateCrimeDto.Description;
            existingCrime.EventPlace = updateCrimeDto.EventPlace;
            existingCrime.Email = updateCrimeDto.Email;
            existingCrime.Status = updateCrimeDto.Status;
            existingCrime.LawEnforcementId = updateCrimeDto.LawEnforcementId;

            await _crimesRepository.UpdateAsync(existingCrime);

            return NoContent();
        }
    }
}
