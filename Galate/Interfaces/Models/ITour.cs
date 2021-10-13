using System;

namespace Galate.Interfaces.Models
{
    public interface ITour
    {
        int Id { get; set; }

        string TourName { get; set; }

        double Price { get; set; }

        DateTime TourDate { get; set; }
    }
}
