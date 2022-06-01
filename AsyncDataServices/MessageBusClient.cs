using System;
using System.Text;
using System.Text.Json;
using CarService.Dtos;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CarService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public MessageBusClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            var factory = new ConnectionFactory() { HostName = this.configuration["RabbitMqHost"], Port = int.Parse(this.configuration["RabbitMQPort"]) };

            try
            {
                this.connection = factory.CreateConnection();
                this.channel = this.connection.CreateModel();

                this.channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                this.connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                System.Console.WriteLine($"--> Connected to MessageBus");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            }
        }
        public void PublishNewCar(CarPublishedDto carPublishedDto)
        {
            var message = JsonSerializer.Serialize(carPublishedDto);

            if (this.connection.IsOpen)
            {
                System.Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ Connections closed, not sending!");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            this.channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

            System.Console.WriteLine($"--> We have sent {message}");
        }

        public void Dispose()
        {
            System.Console.WriteLine("MessageBus Disposed");
            if (this.channel.IsOpen)
            {
                this.channel.Close();
                this.connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            System.Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }
    }
}