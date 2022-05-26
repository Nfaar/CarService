using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Data;
using CarService.Dtos;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [Route("api/car/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarRepo repository;

        private IMapper mapper;

        public CarsController(
            ICarRepo repository,
            IMapper mapper
        )
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarReadDto>> GetCars()
        {
            Console.WriteLine("--> Getting all cars....");

            var allCars = this.repository.GetAllCars();
            return Ok(this.mapper.Map<IEnumerable<CarReadDto>>(allCars));
        }

        [HttpGet]
        public ActionResult<CarReadDto> GetCarById(int id)
        {
            Console.WriteLine("--> Getting a car by id!");
            var singleCar = this.repository.GetCarById(id);
            {
                return Ok(this.mapper.Map<IEnumerable<CarReadDto>>(singleCar));
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarReadDto>> AddCar(
            CarCreateDto carCreateDto)
        {

            // If it is not null store it in the database
            // TODO viknay: check what additional verification is needed
            if (carCreateDto == null)
            {
                throw new ArgumentNullException("Please provide valid input in order to add a car!");
            }


            var carModel = this.mapper.Map<Car>(carCreateDto);
            this.repository.AddCar(carModel);

            var carReadDto = this.mapper.Map<CarReadDto>(carModel);

            // TODO viknay: publish to event bus

            return CreatedAtRoute(nameof(GetCarById), new { carReadDto.Id }, carReadDto);
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