using CommandService.EventProcessor;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using System.Text;

namespace CommandService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
            exchange: "trigger",
            routingKey: "");

            Console.WriteLine("------> Listening on the Message Bus.....");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionSutdown;
        }

        private void RabbitMQ_ConnectionSutdown(object? sender, ShutdownEventArgs e)
        {
            System.Console.WriteLine("--------> Connection Shutdown........");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                System.Console.WriteLine("------> Event Recieved");

                var body = ea.Body;

                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _channel.BasicConsume(queue: _queueName, autoAck : true, consumer : consumer);;
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}