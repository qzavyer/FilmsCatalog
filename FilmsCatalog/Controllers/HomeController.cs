using FilmsCatalog.Data.Contracts;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IFilmsRepository _filmsRepository;

        public HomeController(ILogger<HomeController> logger, IFilmsRepository filmsRepository)
        {
            _logger = logger;
            _filmsRepository = filmsRepository;
        }

        /// <summary>
        /// Отображает таблицу фильмов
        /// </summary>
        /// <param name="page">Номер страницы</param>
        public async Task<IActionResult> Index(int page = 0)
        {
            const int limit = 10;
            var start = page * limit;
            var model = await _filmsRepository.AllFilmsAsync(start, limit);
            var count = await _filmsRepository.CountAsync();

            ViewData["Count"] = count;
            ViewData["Page"] = page;

            return View(model);
        }

        /// <summary>
        /// Удаляет фильм
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public async Task<IActionResult> Delete(int id)
        {
            var film = await _filmsRepository.GetByIdAsync(id);
            await _filmsRepository.DeleteAsync(film);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Сохраняет фильм
        /// </summary>
        /// <param name="model">Данные фильма</param>
        [HttpPost]
        public async Task<IActionResult> Save(Data.Film model)
        {
            if(model.Id == 0)
            {
                await _filmsRepository.AddAsync(model);
            }
            else
            {
                await _filmsRepository.SaveAsync(model);
            }
            
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Добавляет фильм
        /// </summary>
        public IActionResult Add()
        {
            return View(nameof(Edit), new Data.Film());
        }

        /// <summary>
        /// Редактирует фильм
        /// </summary>
        /// <param name="id">Идентификато</param>
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _filmsRepository.GetByIdAsync(id);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
