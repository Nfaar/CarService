using System.Collections.Generic;
using AutoMapper;
using CarService.Data;
using CarService.Dtos;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [Route("api/c/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ICarRepo repository;
        private readonly IMapper mapper;

        public ReservationsController(ICarRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationReadDto>> GetReservations()
        {
            System.Console.WriteLine("--> Getting Reservations from Car Service");

            var reservationItems = this.repository.GetAllReservations();

            return Ok(this.mapper.Map<IEnumerable<ReservationReadDto>>(reservationItems));
        }

        [HttpPost]
        public void CreateReservationForCar(ReservationCreateDto reservationCreateDto, int carId)
        {
            System.Console.WriteLine($"--> Hit CreateReservationForCar. Car Id: {carId}");

            if (this.repository.CarExists(carId))
            {
                var isFound = NotFound();
            }

            var reservation = this.mapper.Map<Reservation>(reservationCreateDto);

            this.repository.CreateReservation(reservation);
            this.repository.SaveChanges();

            var reservationReadDto = this.mapper.Map<ReservationReadDto>(reservation);

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("--> Inbound POST # Car Service");

            return Ok("Inbound test of from Reservations Controller");
        }
    }
}