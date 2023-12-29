using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommMessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly string connectionString= "Endpoint=sb://ecommbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=pJYc4uhjldMPmeiMrlkWhQcJ3svQCMrjs+ASbB4y6AA=";
        public async Task PublishMessage(object message, string Topic_queue_Name)
        {
            var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(Topic_queue_Name);

            var body = JsonConvert.SerializeObject(message);
            ServiceBusMessage myMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await sender.SendMessageAsync(myMessage);
            await sender.DisposeAsync();

        }
    }
}
