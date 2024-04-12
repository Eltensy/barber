using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services.Implementations
{
    public class BarberShopService : IBarberShopService
    {
        private readonly IBarberShopRepository _barbershopRepository;

        public BarberShopService(IBarberShopRepository barbershopRepository)
        {
            _barbershopRepository = barbershopRepository;
        }

        public async Task DeleteBarberShop(int barbershopId)
        {
            await _barbershopRepository.DeleteBarberShop(barbershopId);
        }

        public async Task<BarberShopDto?> GetBarberShopById(int barbershopId)
        {
            var barbershop = await _barbershopRepository.GetBarberShopByID(barbershopId);

            BarberShopDto? barbershopDto = null;
            if (null != barbershop)
            {
                barbershopDto = new BarberShopDto()
                {
                    Id = barbershop.Id,
                    Name = barbershop.Name,
                    Address = barbershop.Address,
                    Phone = barbershop.Phone,
                    PhoneSecond = barbershop.PhoneSecond,
                    Description = barbershop.Description,
                    PhotoUri = barbershop.PhotoUri,
                    SocialUri = barbershop.SocialUri,
                    SocialUriSecond = barbershop.SocialUriSecond,
                    SocialUriThird = barbershop.SocialUriThird,
                };
            }
            return barbershopDto;
        }

        public async Task<BarberShopDto?> GetBarberShopFirst()
        {
            BarberShopDto? barbershopDto = null;
            var barbershop = await _barbershopRepository.GetBarberShopFirst();
            if (barbershop == null) // No info about BarberShop in DB
            {
                throw new Exception("No info about BarberShop in DB");
            }
            else
            {
                barbershopDto = new BarberShopDto()
                {
                    Id = barbershop.Id,
                    Name = barbershop.Name,
                    Address = barbershop.Address,
                    Phone = barbershop.Phone,
                    PhoneSecond = barbershop.PhoneSecond,
                    Description = barbershop.Description,
                    PhotoUri = barbershop.PhotoUri,
                    SocialUri = barbershop.SocialUri,
                    SocialUriSecond = barbershop.SocialUriSecond,
                    SocialUriThird = barbershop.SocialUriThird,
                };
            }

            return barbershopDto;
        }

        public async Task<List<BarberShopDto>> GetBarberShops()
        {
            var barbershops = await _barbershopRepository.GetBarberShops();
            var barbershopsDtos = from barbershop in barbershops
                                  select new BarberShopDto()
                                  {
                                      Id = barbershop.Id,
                                      Name = barbershop.Name,
                                      Address = barbershop.Address,
                                      Phone = barbershop.Phone,
                                      PhoneSecond = barbershop.PhoneSecond,
                                      Description = barbershop.Description,
                                      PhotoUri = barbershop.PhotoUri,
                                      SocialUri = barbershop.SocialUri,
                                      SocialUriSecond = barbershop.SocialUriSecond,
                                      SocialUriThird = barbershop.SocialUriThird,
                                  };
            return barbershopsDtos.ToList();
        }

        public async Task InsertBarberShop(BarberShopDto barbershopDto)
        {
            BarberShop barbershop = new BarberShop()
            {
                Id = barbershopDto.Id,
                Name = barbershopDto.Name,
                Address = barbershopDto.Address,
                Phone = barbershopDto.Phone,
                PhoneSecond = barbershopDto.PhoneSecond,
                Description = barbershopDto.Description,
                PhotoUri = barbershopDto.PhotoUri,
                SocialUri = barbershopDto.SocialUri,
                SocialUriSecond = barbershopDto.SocialUriSecond,
                SocialUriThird = barbershopDto.SocialUriThird,
            };
            await _barbershopRepository.InsertBarberShop(barbershop);
        }

        public async Task UpdateBarberShop(BarberShopDto barbershopDto)
        {
            BarberShop barbershop = new BarberShop()
            {
                Id = barbershopDto.Id,
                Name = barbershopDto.Name,
                Address = barbershopDto.Address,
                Phone = barbershopDto.Phone,
                PhoneSecond = barbershopDto.PhoneSecond,
                Description = barbershopDto.Description,
                PhotoUri = barbershopDto.PhotoUri,
                SocialUri = barbershopDto.SocialUri,
                SocialUriSecond = barbershopDto.SocialUriSecond,
                SocialUriThird = barbershopDto.SocialUriThird,
            };

            await _barbershopRepository.UpdateBarberShop(barbershop);
        }
    }
}
