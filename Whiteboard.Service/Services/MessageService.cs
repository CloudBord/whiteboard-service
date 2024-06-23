using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Whiteboard.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;

        public MessageService(IConfiguration configuration) 
        {
            _configuration = configuration; //= new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        public void TrySendMessage<T>(string queue, T payload, out bool result) 
        {
            var message = JsonSerializer.Serialize<T>(payload);

            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = Int32.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
            };

            using var channel = factory.CreateConnection().CreateModel();

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queue,
                basicProperties: null,
                body: body
            );

            result = true;
        }
    }
}
