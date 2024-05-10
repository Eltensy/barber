namespace DataAccessLayer.Entities
{
    public class Client
    {
        //public required string Id { get; set; }
        //public required string Name { get; set; }
        //public required string Surname { get; set; }
        //public required string Phone { get; set; }
        //public required string Email { get; set; }
        //public required string PasswordHash { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Client() { }

        public Client(ApplicationUser? applicationUser)
        {
            Id = applicationUser.Id ?? "";
            Name = applicationUser.UserName ?? "";
            Surname = "";
            Phone = applicationUser.PhoneNumber ?? "";
            Email = applicationUser.Email ?? "";
            PasswordHash = applicationUser.PasswordHash ?? "";
        }

        public Client ApplicationUserToClient(ApplicationUser applicationUser)
        {
            Client client = new Client()
            {
                Id = applicationUser.Id,
                Name = applicationUser.UserName ?? "",
                Surname = "",
                Phone = applicationUser.PhoneNumber ?? "",
                Email = applicationUser.Email ?? "",
                PasswordHash = applicationUser.PasswordHash ?? "",
            };

            return client;
        }

    }
}
