using AutoMapper;
using CarService.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [Route("api/car/[controller]")]
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

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("--> Inbound POST # Car Service");

            return Ok("Inbound test of from Reservations Controller");
        }
    }
}