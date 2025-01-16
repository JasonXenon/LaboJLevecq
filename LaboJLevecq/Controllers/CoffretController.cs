using Demo = LaboJLevecq.Models;
using Dal = DAL.Entities;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LaboJLevecq.Tools;
using DAL.Interface;
using LaboJLevecq.Models;

namespace LaboJLevecq.Controllers
{
    public class CoffretController : Controller
    {
        private ICoffretRepository CoffretRepository;
        private IGenreRepository GenreRepository;
        public CoffretController(ICoffretRepository coffretRepository, IGenreRepository genreRepository)
        {
            CoffretRepository = coffretRepository;
            GenreRepository = genreRepository;
        }

        [ViewData]
        public string Title { get; set; }

        public IActionResult Index()
        {
            IEnumerable<CoffretListItem> model = CoffretRepository.GetAll()
                .Select(c => new CoffretListItem() { Id = c.Id, Titre = c.Titre, Prix = c.Prix, Quantite = c.Quantite, Synopsis = c.Synopsis, AfficheUrl = c.AfficheUrl });
            return View(model);
        }


        public IActionResult Details(int id)
        {

            try
            {
                Coffret model = CoffretRepository.GetById(id).ToASP();
                Console.WriteLine($"Quantité disponible pour le coffret '{model.Titre}': {model.Quantite}");
                Title = $"Vue détaillée de {model.Titre}";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult CoffretForm()
        {
            var genres = GenreRepository.GetAll()
                .Select(g => new GenreListItem() { Id = g.Id, Label = g.Label }).ToList();

            var model = new CoffretForm
            {
                Genres = genres
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CoffretForm(CoffretForm coffretForm)
        {
            if (!ModelState.IsValid)
            {
                return View(coffretForm);
            }

            try
            {
                CoffretRepository.Create(coffretForm.ToDal());
                // Ajout du message de réussite dans TempData
                TempData["SuccessMessage"] = "Le coffret a été ajouté avec succès.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Une erreur est survenue lors de l'ajout du coffret : " + ex.Message);
                return View(coffretForm);
            }

            return RedirectToAction(nameof(coffretForm));
        }


        public IActionResult Delete(int id)
        {
            CoffretRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Coffret model = CoffretRepository.GetById(id).ToASP();
            return View(model);

        }

        [HttpPost]
        public IActionResult Update(Coffret model)
        {
            CoffretRepository.Update(model.ToDal());
            return RedirectToAction(nameof(Index));
        }

    }
}