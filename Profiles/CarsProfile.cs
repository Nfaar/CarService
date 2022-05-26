using AutoMapper;
using CarService.Dtos;
using CarService.Models;

namespace CarService.Profiles
{
    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            CreateMap<Car, CarReadDto>();
            CreateMap<CarCreateDto, Car>();
            CreateMap<Reservation, ReservationReadDto>();
        }
    }
}