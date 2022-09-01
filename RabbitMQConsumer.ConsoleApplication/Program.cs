using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
using Store.Domain.Dtos;
using RabbitMQ.Client.Events;
using Store.Business.Utilities;

var factory = new ConnectionFactory // Definindo uma conexão com um nó RabbitMQ
{
    HostName = "127.0.0.1",
    UserName = "admin",
    Password = "Senha12345"
};

using (var connection = factory.CreateConnection()) // Abrindo uma conexão com o nó definido
{

    using (var channel = connection.CreateModel()) // Criação do canal onde a fila será definida
    {
        channel.QueueDeclare(queue: "orderQueue", // => Nome da Fila que será consumida
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel); // Solicita a entrega das mensagens de forma assíncrona.
        
        consumer.Received += (model, eventArgs) => 
        {
            try
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body); // Recebe a mensagem da fila
                var order = JsonConvert.DeserializeObject<Dictionary<string, CheckOutDto>>(message);
                Console.WriteLine($"Product message received: {message}");

                CheckoutUtilities.CreateOrder(order);

                channel.BasicAck(eventArgs.DeliveryTag, false); // Deu tudo certo
            }
            catch
            {
                channel.BasicNack(eventArgs.DeliveryTag, false, true); // Se falhar, devolve para a fila
            }
        };

        channel.BasicConsume(queue: "orderQueue", autoAck: false, consumer: consumer);
        Console.ReadKey();
    } ;    
} ;

