namespace Galate.Interfaces.Models
{
    public interface IClient
    {
        int Id { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        string Phone { get; set; }
    }
}
