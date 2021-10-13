namespace Galate.Interfaces.Models
{
    public interface IUser
    {
        int Id { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string Email { get; set; }

        string Login { get; set; }

        string Password { get; set; }

        bool IsAdmin { get; set; }
    }
}
