using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Car
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        [Required]
        public int Mileage { get; set; }

        public double HourlyPrice { get; set; }

        public DateTime AvailableFrom { get; set; }

        public DateTime AvailableUntil { get; set; }

        public ICollection<Reservation> Reservation { get; set; } = new List<Reservation>();
    }
}