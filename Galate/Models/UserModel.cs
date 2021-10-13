using Galate.Interfaces.Models;

namespace Galate.Models
{
    public sealed class UserModel : IUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
