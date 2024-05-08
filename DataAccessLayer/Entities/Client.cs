namespace DataAccessLayer.Entities
{
    public class Client
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

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
