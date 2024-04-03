using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;

        public VisitService(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<IEnumerable<VisitDto>> GetVisits()
        {
            var visits = await _visitRepository.GetVisits();
            var visitsDtos = from visit in visits
                             select new VisitDto()
                             { 
                                 Id = visit.Id,
                                 fk_CLientId = visit.fk_CLientId,
                                 fk_GuestId = visit.fk_GuestId,
                                 fk_BarberId = visit.fk_BarberId,
                                 fk_ServiceId = visit.fk_ServiceId,
                                 Date = visit.Date,
                                 Time = visit.Time
                             };

            return visitsDtos.ToList();
        }

        public async Task<VisitDto?> GetVisitByID(int visitId)
        {
            Visit? visit = await _visitRepository.GetVisitByID(visitId);
            VisitDto? visitDto = null;
            if(visit != null) 
            {
                visitDto = new VisitDto()
                {
                    Id = visit.Id,
                    fk_CLientId = visit.fk_CLientId,
                    fk_GuestId = visit.fk_GuestId,
                    fk_BarberId = visit.fk_BarberId,
                    fk_ServiceId = visit.fk_ServiceId,
                    Date = visit.Date,
                    Time = visit.Time
                };
            }
            return visitDto;
        }

        public async Task InsertVisit(VisitDto visitDto)
        {
            Visit visit = new Visit()
            {
                Id = visitDto.Id,
                fk_CLientId = visitDto.fk_CLientId,
                fk_GuestId = visitDto.fk_GuestId,
                fk_BarberId = visitDto.fk_BarberId,
                fk_ServiceId = visitDto.fk_ServiceId,
                Date = visitDto.Date,
                Time = visitDto.Time
            };

            await _visitRepository.InsertVisit(visit);
        }

        public async Task DeleteVisit(int visitId)
        {
            await _visitRepository.DeleteVisit(visitId);
        }

        public async Task UpdateVisit(VisitDto visitDto)
        {
            Visit visit = new Visit()
            {
                Id = visitDto.Id,
                fk_CLientId = visitDto.fk_CLientId,
                fk_GuestId = visitDto.fk_GuestId,
                fk_BarberId = visitDto.fk_BarberId,
                fk_ServiceId = visitDto.fk_ServiceId,
                Date = visitDto.Date,
                Time = visitDto.Time
            };
            await _visitRepository.UpdateVisit(visit);
        }
    }
}
