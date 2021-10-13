using Galate.Interfaces.Models;
using System;

namespace Galate.Models
{
    public sealed class TourModel : ITour
    {
        public int Id { get; set; }

        public string TourName { get; set; }

        public double Price { get; set; }

        public string FormatPrice { get; set; }

        public DateTime TourDate { get; set; }

        public string StringTourDate { get; set; }
    }
}
