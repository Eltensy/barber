//using Microsoft.AspNetCore.Mvc;
//using BarberLayered.Models;
//using BusinessLogicLayer.Services.Interfaces;
//using BusinessLogicLayer.Services.Implementations;

//namespace BarberLayered.Controllers
//{
//    public class ClientHomeController : Controller
//    {
//        private readonly IClientService _clientService;
//        private Client? _client;
//        private BarberShop? _barberShop;

//        public ClientHomeController(IClientService clientService)
//        {
//            _clientService = clientService;
//            _client = null;
//            _barberShop = null;
//        }

//        public async Task<IActionResult> Index(Client client)
//        {
//            //var barberShop = await _clientService.();

//            if (barberShop != null)
//                _barberShop = new BarberShop(barberShop);

//            _client = client;

//            ViewBag.Client = _client;

            //string path = "/Barbers/ClientBarbers";

            //ViewData["Path"] = path;

//            return View(_barberShop);
//        }

//        public async Task<IActionResult> ClientAccount(Client client)
//        {
//            _client = client;

//            return View(_client);
//        }
//    }
//}

