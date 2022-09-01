using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Store.API.Interfaces;

namespace Store.API.Services
{
    public class RabitMQPublisher : IRabitMQPublisher
    {
        public void SendProductMessage<T>(T message, string userId)
        {
            try
            {
                var resultMessage = new Dictionary<string, object>()
            {
                {userId, message}
            };

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
                        channel.QueueDeclare(queue: "orderQueue", // => Nome da Fila
                                             durable: false,      // => Se true, a fila permanece ativa após o servidor reiniciar
                                             exclusive: false,    // => Se true, só pode ser acessada na conexão atual, sendo excluída após fechar a conexão
                                             autoDelete: false,   // => Se true, será deletada automaticamente após os consumidores usarem a fila
                                             arguments: null);

                        var json = JsonConvert.SerializeObject(resultMessage); //message
                        var body = Encoding.UTF8.GetBytes(json);
                        channel.BasicPublish(exchange: "", routingKey: "orderQueue", body: body); // Publicação: Possui o nome da fila que será publicada e o corpo da mensagem.
                    };
                };
            }
            catch (Exception)
            {
                throw;
            }           
        }
    }
}
