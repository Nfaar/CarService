using System.Collections.Generic;
using CarService.Models;

namespace CarService.Data
{
    public interface ICarRepo
    {
        bool SaveChanges();

        // Cars
        IEnumerable<Car> GetAllCars();

        Car GetCarById(int Id);

        Car GetCarForReservation(string ReservationNumber);

        void AddCar(Car car);

        void DeleteCarById(int Id);

        // Reservations

        IEnumerable<Reservation> GetAllReservations();
        void CreateReservation(Reservation reservation);

        bool CarExists(int carId);

        IEnumerable<Reservation> GetReservationsForCar(int carId);
    }
}