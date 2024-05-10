using DataAccessLayer.Entities;

namespace BusinessLogicLayer.DTOs
{
    public class BarberShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? PhoneSecond { get; set; }
        public string? Description { get; set; }
        public string? PhotoUri { get; set; }
        public string? SocialUri { get; set; }
        public string? SocialUriSecond { get; set; }
        public string? SocialUriThird { get; set; }

        public BarberShopDto() { }

        public BarberShopDto(BarberShop barbershop)
        {
            Id = barbershop.Id;
            Name = barbershop.Name;
            Address = barbershop.Address;
            Phone = barbershop.Phone;
            PhoneSecond = barbershop.PhoneSecond;
            Description = barbershop.Description;
            PhotoUri = barbershop.PhotoUri;
            SocialUri = barbershop.SocialUri;
            SocialUriSecond = barbershop.SocialUriSecond;
            SocialUriThird = barbershop.SocialUriThird;
        }

        public BarberShop ToEntity()
        {
            BarberShop barbershop = new BarberShop()
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address,
                Phone = this.Phone,
                PhoneSecond = this.PhoneSecond,
                Description = this.Description,
                PhotoUri = this.PhotoUri,
                SocialUri = this.SocialUri,
                SocialUriSecond = this.SocialUriSecond,
                SocialUriThird = this.SocialUriThird,
            };

            return barbershop;
        }
    }
}
