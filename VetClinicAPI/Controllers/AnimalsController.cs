using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VetClinicAPI.Models;

namespace VetClinicAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private static List<Animal> animals = new List<Animal>();
        private static int nextAnimalId = 1;
        private static int nextVisitId = 1;

        [HttpGet]
        public IActionResult GetAnimals()
        {
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            animal.Id = nextAnimalId++;
            animals.Add(animal);
            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound();

            animal.Name = updatedAnimal.Name;
            animal.Category = updatedAnimal.Category;
            animal.Weight = updatedAnimal.Weight;
            animal.FurColor = updatedAnimal.FurColor;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound();

            animals.Remove(animal);
            return NoContent();
        }

        [HttpPost("{animalId}/visits")]
        public IActionResult AddVisit(int animalId, Visit visit)
        {
            var animal = animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null) return NotFound("Animal not found.");

            visit.Id = nextVisitId++;
            animal.Visits.Add(visit);
            return CreatedAtAction(nameof(GetVisit), new { animalId = animalId, visitId = visit.Id }, visit);
        }

        [HttpGet("{animalId}/visits")]
        public IActionResult GetVisits(int animalId)
        {
            var animal = animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null) return NotFound("Animal not found.");

            return Ok(animal.Visits);
        }

        [HttpGet("{animalId}/visits/{visitId}")]
        public IActionResult GetVisit(int animalId, int visitId)
        {
            var animal = animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null) return NotFound("Animal not found.");

            var visit = animal.Visits.FirstOrDefault(v => v.Id == visitId);
            if (visit == null) return NotFound("Visit not found.");

            return Ok(visit);
        }
    }
