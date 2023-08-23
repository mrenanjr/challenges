using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateS.Domain.Interfaces;
using TemplateS.Infra.CrossCutting.RabbitMQ.Options;

namespace TemplateS.Infra.CrossCutting.RabbitMQ.Repositories
{
    public class RepositoryMessage<TEntity> : IRepositoryMessage<TEntity> where TEntity : class
    {
        private readonly RabbitMqConfiguration _config;
        private IConnection _connection;

        public RepositoryMessage(IOptions<RabbitMqConfiguration> options)
        {
            _config = options.Value;

            if(_config == null)
            {
                _config.Host = "localhost";
                _config.Queue = "messages";
                _config.User = "guest";
                _config.Port = 5672;
                _config.Password = "guest";
            }

            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory() {
                    HostName = _config.Host,
                    UserName = _config.User,
                    Password = _config.Password,
                    Port = _config.Port,
                    DispatchConsumersAsync = true
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity AddMessage(TEntity viewModel)
        {
            try
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _config.Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var stringfiedMessage = JsonConvert.SerializeObject(viewModel);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                    channel.BasicPublish(exchange: "", routingKey: _config.Queue, basicProperties: null, body: bytesMessage);
                }

                return viewModel;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public TEntity GetMessage()
        {
            try
            {
                TEntity? message = null;
                using (var channel = _connection.CreateModel())
                {
                    var autoAck = false;
                    var consumer = new EventingBasicConsumer(channel);
                    var result = channel.BasicGet(_config.Queue, autoAck);
                    
                    if(result != null)
                    {
                        var props = result.BasicProperties;
                        var content = Encoding.UTF8.GetString(result.Body.ToArray());
                        
                        message = JsonConvert.DeserializeObject<TEntity>(content);
                        channel.BasicAck(result.DeliveryTag, false);
                    }
                }

                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                if (_connection != null)
                    _connection.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
