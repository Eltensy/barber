using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services.Implementations
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _barberRepository;

        public BarberService(IBarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public async Task DeleteBarber(int barberId)
        {
            await _barberRepository.DeleteBarber(barberId);
        }

        public async Task<BarberDto?> GetBarberById(int barberId)
        {
            var barber = await _barberRepository.GetBarberByID(barberId);

            BarberDto? barberDto = null;
            if (null != barber)
            {
                barberDto = new BarberDto()
                {
                    Id = barber.Id,
                    Name = barber.Name,
                    Surname = barber.Surname,
                    Phone = barber.Phone,
                    Email = barber.Email,
                    PasswordHash = barber.PasswordHash,
                    PhotoUri = barber.PhotoUri,
                    Description = barber.Description,
                    PortfolioUri = barber.PortfolioUri
                };
            }
            return barberDto;
        }

        public async Task<List<BarberDto>> GetBarbers()
        {
            var barbers = await _barberRepository.GetBarbers();
            var barbersDtos = from barber in barbers
                              select new BarberDto()
                              {
                                  Id = barber.Id,
                                  Name = barber.Name,
                                  Surname = barber.Surname,
                                  Phone = barber.Phone,
                                  Email = barber.Email,
                                  PasswordHash = barber.PasswordHash
                              };
            return barbersDtos.ToList();
        }

        public async Task InsertBarber(BarberDto barberDto)
        {
            Barber barber = new Barber()
            {
                Id = barberDto.Id,
                Name = barberDto.Name,
                Surname = barberDto.Surname,
                Phone = barberDto.Phone,
                Email = barberDto.Email,
                PasswordHash = barberDto.PasswordHash
            };
            await _barberRepository.InsertBarber(barber);
        }

        public async Task UpdateBarber(BarberDto barberDto)
        {
            Barber barber = new Barber()
            {
                Id = barberDto.Id,
                Name = barberDto.Name,
                Surname = barberDto.Surname,
                Phone = barberDto.Phone,
                Email = barberDto.Email,
                PasswordHash = barberDto.PasswordHash
            };

            await _barberRepository.UpdateBarber(barber);
        }

        public async Task<BarberDto?> GetBarberByEmail(string email)
        {
            Barber? barber = await _barberRepository.GetBarberByEmail(email);
            BarberDto? barberDto = null;
            if (barber != null)
            {
                barberDto = new BarberDto()
                {
                    Id = barber.Id,
                    Name = barber.Name,
                    Surname = barber.Surname,
                    Email = barber.Email,
                    Phone = barber.Phone,
                    PasswordHash = barber.PasswordHash,
                    PhotoUri = barber.PhotoUri,
                    Description = barber.Description,
                    PortfolioUri = barber.PortfolioUri
                };
            }

            return barberDto;
        }
    }
}
