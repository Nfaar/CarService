using CarService.Dtos;

namespace CarService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewCar(CarPublishedDto carPublishedDto);
    }
}