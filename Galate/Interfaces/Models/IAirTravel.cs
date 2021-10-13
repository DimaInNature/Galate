namespace Galate.Interfaces.Models
{
    public interface IAirTravel
    {
        int Id { get; set; }

        int TourId { get; set; }

        int ClientId { get; set; }
    }
}
