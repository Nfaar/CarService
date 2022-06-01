using AutoMapper;
using CarService.Dtos;
using CarService.Models;

namespace CarService.Profiles
{
    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            // Cars
            CreateMap<Car, CarReadDto>();
            CreateMap<CarCreateDto, Car>();
            CreateMap<CarReadDto, CarPublishedDto>();

            //Reservations
            CreateMap<Reservation, ReservationReadDto>();
            CreateMap<ReservationCreateDto, Reservation>();
        }
    }
}