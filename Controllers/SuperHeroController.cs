using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.API.Repository;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperHeroController : ControllerBase
    {
    //     private static List<SuperHero> heroes = new List<SuperHero>()
    //     {
    //         new SuperHero { 
    //             Id = 1, 
    //             Name = "Batman",
    //             FirstName = "Bruce",
    //             LastName = "Wayne",
    //             Place = "Gotham City",
    //         },
    //         new SuperHero { 
    //             Id = 2, 
    //             Name = "Thor",
    //             FirstName = "Thor",
    //             LastName = "Thor",
    //             Place = "Asgard",
    //         },
    //         new SuperHero { 
    //             Id = 3, 
    //             Name = "Capitão América",
    //             FirstName = "Steve",
    //             LastName = "Rogers",
    //             Place = "Terra",
    //         }
    //     };

        public readonly HeroRepository _repository;
        public SuperHeroController(HeroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> FindAllSuperHero()
        {
            var heroes = await _repository.FindAllSuperHero();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> FindSpecificSuperHero(int id)
        {
            var hero = await _repository.FindSpecificSuperHero(id);

            if(hero == null) 
                return BadRequest("Hero not found");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddSuperHero(SuperHero hero)
        {
            var heroListCreated = await _repository.AddSuperHero(hero);
            return Ok(heroListCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero request, int id)
        {
            var hero = await FindSpecificSuperHero(id);
            
            if(hero == null)
                return BadRequest("Hero not found");

            var heroesListUpdated = await _repository.UpdateSuperHero(request, id);
            return Ok(heroesListUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = await FindSpecificSuperHero(id);
            
            if(dbHero == null)
                return BadRequest("Hero not found");

            var heroesListDeleted = _repository.DeleteSuperHero(id);
            return Ok(heroesListDeleted);
        }
    }
}