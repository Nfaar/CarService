namespace CarService.Dtos
{
    public class ReservationCreateDto
    {
        public string Cost { get; set; }
        public string ReservationNumber { get; set; }

        public int CarId { get; set; }
    }
}