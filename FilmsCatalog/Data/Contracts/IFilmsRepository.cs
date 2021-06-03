using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmsCatalog.Data.Contracts
{
    public interface IFilmsRepository
    {
        Task<IEnumerable<Film>> AllFilmsAsync(int start, int limit);
        Task<Film> AddAsync(Film film);
        Task<Film> SaveAsync(Film film);

        Task DeleteAsync(Film film);

        Task<Film> GetByIdAsync(int id);

        Task<int> CountAsync();
    }
}
