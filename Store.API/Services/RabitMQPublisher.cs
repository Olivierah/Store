using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Store.API.Interfaces;
using System.Text;

namespace Store.API.Services
{
    public class RabitMQPublisher : IRabitMQPublisher
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                UserName = "admin",
                Password = "Senha12345"
            };
            var connection = factory.CreateConnection();

            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
