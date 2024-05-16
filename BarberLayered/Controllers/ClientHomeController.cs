using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using BarberShop = BarberLayered.Models.BarberShop; 
using Client = BarberLayered.Models.Client; 

namespace BarberLayered.Controllers
{
    public class ClientHomeController : Controller
    {
        private readonly IBarberShopService _barberShopService;
        private readonly IClientService _clientService;
        private BarberShop? _barberShop;
        private Client? _client;

        public ClientHomeController(IBarberShopService barberShopService, IClientService clientService)
        {
            _barberShopService = barberShopService;
            _clientService = clientService;
            _barberShop = null;
            _client = null;
        }
        public async Task<IActionResult> Index(Client client)
        {
            _client = client;

            var barberShop = await _barberShopService.GetBarberShopFirst();

            if (barberShop != null)
                _barberShop = new BarberShop(barberShop);

            ViewBag.Client = _client;

            return View(_barberShop);
        }

        public async Task<IActionResult> ClientAccount(int clientId) 
        {
            var client = await _clientService.GetClientById(clientId);

            if (client == null)
            {
                Log.Error("No info about Client with id={Id} was found in the DataBase", clientId);
            }
            else
            {
                _client = new Client(client);
            }

            ViewBag.Client = _client;
            return View(_client);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(Client client)
        {
            if (ModelState.IsValid)
            {
                ClientDto clientDto = new ClientDto()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Phone = client.Phone,
                    Email = client.Email,
                };

                try
                {
                    // Update the client
                    await _clientService.UpdateClient(clientDto);

                    TempData["SuccessMessage"] = "Your data has been successfully updated!";

                }
                catch (Exception ex)
                {
                    // Handling errors during password change
                    TempData["ErrorMessage"] = "Incorrectly entered data";
                }
            }
            else
            {
                //TempData["ErrorMessage"] = "Incorrectly entered data";
            }

            return RedirectToAction("ClientAccount", new { clientId = client.Id });

        }

    }
}

