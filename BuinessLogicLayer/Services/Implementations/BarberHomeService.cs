using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementations
{
    public class BarberHomeService: IBarberHomeService
    {
        private readonly IVisitService _visitService;
        private readonly IServiceService _serviceService;
        private readonly IGuestService _guestService;
        private readonly IClientService _clientService;

        public BarberHomeService(IVisitService visitService, IServiceService serviceService,
            IGuestService guestService, IClientService clientService)
        {
            _visitService = visitService;
            _serviceService = serviceService;
            _guestService = guestService;
            _clientService = clientService;
        }
        public async Task<List<VisitExtDto>> GetVisitsByBarberId(int fk_BarberId)
        {
            var visits = await _visitService.GetVisitsByBarberId(fk_BarberId);
            List<VisitExtDto> _visits = new List<VisitExtDto>();
            if(visits.Any())
            {
                foreach(VisitDto visit in visits)
                {
                    ServiceDto service = await _serviceService.GetServiceByID(visit.fk_ServiceId);
                    if(service == null)
                    {
                        Log.Error("No service with ID={ID} found in the DB!", visit.fk_ServiceId);
                    }

                    string fullname = "";

                    if (visit.fk_GuestId == null)
                    {
                        ClientDto client = await _clientService.GetClientById(visit.fk_ClientId ?? default);
                        fullname = client.Name + " " + client.Surname;
                    }
                    else
                    {
                        GuestDto guest = await _guestService.GetGuestById(visit.fk_GuestId ?? default);
                        fullname = guest.Name + " " + guest.Surname;
                    }

                    _visits.Add(new VisitExtDto
                    {
                        Id = visit.Id,
                        fk_ClientId = visit.fk_ClientId,
                        fk_GuestId = visit.fk_GuestId,
                        fk_BarberId = visit.fk_BarberId,
                        fk_ServiceId = visit.fk_ServiceId,
                        Date = visit.Date,
                        Time = visit.Time,
                        VisitorFullName = fullname,
                        ServiceTitle = service.Title,
                        StartTime = visit.Time,
                        EndTime = visit.Time.Add(service.Duration.ToTimeSpan())
                    });

                }
            }

            return _visits;
        }
    }
}
