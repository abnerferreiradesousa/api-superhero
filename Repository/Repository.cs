using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Blog.API.Repository;

namespace Blog.API.Repository
{
    public class HeroRepository
    {
        public DataContext _context { get; set; }
        public HeroRepository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<List<SuperHero>> FindAllSuperHero()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return heroes;
        }

        public async Task<SuperHero?> FindSpecificSuperHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            return hero;
        }

        public async Task<List<SuperHero>> AddSuperHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            _context.SaveChanges();
            var heroes = await FindAllSuperHero();
            return heroes;
        }

        public async Task<List<SuperHero>> UpdateSuperHero(SuperHero request, int id)
        {
            var hero = await FindSpecificSuperHero(id);

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;

            await _context.SaveChangesAsync();

            var heroes = await FindAllSuperHero();
            return heroes;
        }

        public async Task<List<SuperHero>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);

            _context.SuperHeroes.Remove(dbHero);

            await _context.SaveChangesAsync();

            var heroes = await FindAllSuperHero();
            return heroes;        }
    }
}