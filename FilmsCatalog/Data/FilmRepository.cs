using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmsCatalog.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalog.Data
{
    public class FilmRepository : IFilmsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FilmRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Film> AddAsync(Film film)
        {
            var newFilm = _dbContext.Set<Film>().Add(film);

            await _dbContext.SaveChangesAsync();

            return newFilm.Entity;
        }

        public async Task<IEnumerable<Film>> AllFilmsAsync(int start, int limit)
        {
            return await _dbContext.Set<Film>().Skip(start).Take(limit).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<Film>().CountAsync();
        }

        public async Task DeleteAsync(Film film)
        {
            _dbContext.Set<Film>().Remove(film);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Film>().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Film> SaveAsync(Film film)
        {
            var newFilm = _dbContext.Set<Film>().Update(film);

            await _dbContext.SaveChangesAsync();

            return newFilm.Entity;
        }
    }
}
