using GeekBurguer_Promotion_Infrastructure.Queue.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace GeekBurguer_Promotion_Infrastructure.Queue
{
    public class ServiceBus : IServiceBus
    {
        private IConfiguration _config;
        private QueueClient _queueClient;

        public ServiceBus(IConfiguration config)
        {
            _config = config;
        }

        public Task<bool> ScheduleMessageAsync<T>(T obj, string queueName, DateTime scheduledTime)
        {
            try
            {
                _queueClient = QueueClient(queueName);

                var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                var mBytes = Encoding.UTF8.GetBytes(jsonObj);

                var message = new Message(mBytes);

                _queueClient.ScheduleMessageAsync(message, scheduledTime);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<bool> ScheduleMessageAsync<T>(string serializedObject, string queueName, DateTime scheduledTime)
        {
            try
            {
                _queueClient = QueueClient(queueName);

                var mBytes = Encoding.UTF8.GetBytes(serializedObject);

                var message = new Message(mBytes);

                _queueClient.ScheduleMessageAsync(message, scheduledTime);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<bool> SendAsync<T>(T obj, string queueName)
        {
            try
            {
                _queueClient = QueueClient(queueName);

                var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                var mBytes = Encoding.UTF8.GetBytes(jsonObj);

                var message = new Message(mBytes);

                _queueClient.SendAsync(message).Wait();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<bool> SendAsync<T>(string serializedObject, string queueName)
        {
            try
            {
                _queueClient = QueueClient(queueName);

                var mBytes = Encoding.UTF8.GetBytes(serializedObject);

                var message = new Message(mBytes);

                _queueClient.SendAsync(message).Wait();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public QueueClient QueueClient(string queueName)
        {
            var client = new QueueClient(_config.GetSection("ServiceBusConnectionString").Value, queueName,
                 ReceiveMode.ReceiveAndDelete);
            return client;
        }
    }
}
