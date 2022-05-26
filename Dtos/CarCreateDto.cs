using System;

namespace CarService.Dtos
{
    public class CarCreateDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public double HourlyPrice { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }
    }
}