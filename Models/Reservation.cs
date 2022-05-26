using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Reservation
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Cost { get; set; }
        [Required]
        public string ReservationNumber { get; set; }

        public Car Car { get; set; }
    }
}