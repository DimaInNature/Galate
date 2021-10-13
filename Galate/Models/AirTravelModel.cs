using Galate.Interfaces.Models;

namespace Galate.Models
{
    public sealed class AirTravelModel : IAirTravel
    {
        public int Id { get; set; }

        public int TourId { get; set; }

        public int ClientId { get; set; }

        public TourModel Tour { get; set; }

        public ClientModel Client { get; set; }
    }
}