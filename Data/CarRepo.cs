using System;
using System.Collections.Generic;
using System.Linq;
using CarService.Models;

namespace CarService.Data
{
    public class CarRepo : ICarRepo
    {
        private readonly AppDbContext context;

        public CarRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            System.Console.WriteLine($"Saving car with id: {car.Id} and model {car.Make}");

            this.context.Add(car);
            this.context.SaveChanges();
        }

        public void CreateReservation(Reservation reservation)
        {
            this.context.Reservations.Add(reservation);
            this.context.SaveChanges();
        }

        public void DeleteCarById(int Id)
        {
            var car = this.context.Cars.FirstOrDefault(r => r.Id == Id);
            if (car != null)
            {
                this.context.Cars.Remove(car);
                this.context.SaveChanges();
            }
            else
            {
                throw new Exception("There is no existing car with that id.");
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            return this.context.Cars.ToList();
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return this.context.Reservations.ToList();
        }

        public Car GetCarById(int Id)
        {
            return this.context.Cars.FirstOrDefault(r => r.Id == Id);
        }

        public Car GetCarForReservation(string ReservationNumber)
        {
            return this.context.Reservations.FirstOrDefault(rn => rn.ReservationNumber == ReservationNumber).Car;
        }

        public bool CarExists(int carId)
        {
            return this.context.Cars.Any(rn => rn.Id == carId);
        }

        public bool SaveChanges()
        {
            return (this.context.SaveChanges() >= 0);
        }

        public IEnumerable<Reservation> GetReservationsForCar(int carId)
        {
            return this.context.Reservations.Where(r => r.Car.Id == carId);
        }
    }
}