using Demo = LaboJLevecq.Models;
using DAL.Entities;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using DAL.Interface;
using LaboJLevecq.Tools;

namespace LaboJLevecq.Controllers
{
    public class GenreController : Controller
    {
        private IGenreRepository GenreRepository;

        public GenreController(IGenreRepository repo)
        {
            GenreRepository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenreForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(Demo.Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(GenreForm));
            }
            try
            {
                GenreRepository.Create(genre.ToDal());

                // Ajouter un message de succès
                TempData["successMessage"] = "Le genre a bien été ajouté.";
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                return View(nameof(GenreForm));
            }

            return RedirectToAction(nameof(GenreForm));
        }

    }
}
