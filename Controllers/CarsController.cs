using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarService.AsyncDataServices;
using CarService.Data;
using CarService.Dtos;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [Route("api/c/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarRepo repository;

        private IMapper mapper;
        private readonly IMessageBusClient messageBusClient;

        public CarsController(
            ICarRepo repository,
            IMapper mapper,
            IMessageBusClient messageBusClient
        )
        {
            this.repository = repository;
            this.mapper = mapper;
            this.messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarReadDto>> GetCars()
        {
            Console.WriteLine("--> Getting all cars....");

            var allCars = this.repository.GetAllCars();
            return Ok(this.mapper.Map<IEnumerable<CarReadDto>>(allCars));
        }


        [HttpGet("{carId}")]
        public ActionResult<CarReadDto> GetCarById(int carId)
        {
            Console.WriteLine($"--> Getting a car by id: {carId}");
            var singleCar = this.repository.GetCarById(carId);
            {
                return Ok(this.mapper.Map<CarReadDto>(singleCar));
            }
        }

        [HttpGet("{carId}/reservations")]
        public ActionResult<IEnumerable<ReservationReadDto>> GetReservationsForCar(int carId)
        {
            System.Console.WriteLine($"--> Hit GetReservationsForCar: {carId}");

            if (!this.repository.CarExists(carId))
            {
                return NotFound();
            }

            var reservations = this.repository.GetReservationsForCar(carId);

            return Ok(this.mapper.Map<IEnumerable<ReservationReadDto>>(reservations));
        }

        [HttpPost]
        public ActionResult<CarCreateDto> AddCar(
            CarCreateDto carCreateDto)
        {
            System.Console.WriteLine("--> Hit AddCar HttpPost!");

            // If it is not null store it in the database
            if (carCreateDto == null)
            {
                throw new ArgumentNullException("Please provide valid input in order to add a car!");
            }


            var carModel = this.mapper.Map<Car>(carCreateDto);
            this.repository.AddCar(carModel);

            var carReadDto = this.mapper.Map<CarReadDto>(carModel);

            // TODO viknay: publish to event bus
            try
            {
                var carPublishedDto = this.mapper.Map<CarPublishedDto>(carReadDto);
                carPublishedDto.Event = "Car_Published";
                this.messageBusClient.PublishNewCar(carPublishedDto);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"--> Could not send asynchonously: {ex.Message}");
            }
            System.Console.WriteLine("NameOf:" + nameof(GetCarById));
            System.Console.WriteLine("Id" + carReadDto.Id);
            System.Console.WriteLine("Object" + carReadDto);

            return Ok(carReadDto);
        }

        [HttpDelete]
        public void DeleteSingleReservation(int id)
        {
            if (id < 0)
            {
                throw new Exception("Ids cannot be negative!");
            }

            try
            {
                this.repository.DeleteCarById(id);
                this.repository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid reservation Id.", ex);
            }
        }
    }
}