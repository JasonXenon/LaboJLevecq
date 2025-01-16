using DAL.Entities;
using DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LaboJLevecq.Controllers
{
    public class ClientController : Controller
    {

        private readonly IClientRepository _clientRepository;


        public IActionResult Index()
        {
            return View();
        }


        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // Action GET pour afficher le formulaire de création
        public IActionResult Create()
        {
            return View();
        }

        // Action POST pour ajouter un client
        [HttpPost]
        public IActionResult Create(Client client)
        {
            try
            {
                // Hachage du mot de passe avant d'ajouter à la base de données
                client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

                // Appel au repository pour ajouter le client
                _clientRepository.Create(client);

                // Si l'ajout est réussi, envoyer un message de succès dans TempData
                TempData["SuccessMessage"] = "Client ajouté avec succès.";
                return RedirectToAction("Create");  // Redirige vers une page d'index, remplace si nécessaire
            }
            catch (Exception ex)
            {
                // En cas d'erreur, envoyer un message d'erreur dans TempData
                TempData["ErrorMessage"] = $"Erreur de création du client: {ex.Message}";

                // Retourner la même vue avec le modèle actuel
                return View(client);
            }
        }
    }
}
