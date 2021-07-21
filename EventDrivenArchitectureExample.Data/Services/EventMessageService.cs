using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventDrivenArchitectureExample.Data.Settings;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Data.Services
{
    public class EventMessageService
    {
        public async Task SendEvent<T>(T message, string hubname)
        {
            var eventHubConnectionString = ApplicationSettings.EventHubConnection;
            var eventHubName = hubname;

            var producerClient = new EventHubProducerClient(eventHubConnectionString, eventHubName, new EventHubProducerClientOptions
            {
                RetryOptions = new EventHubsRetryOptions
                {
                    MaximumRetries = 5,
                    Delay = TimeSpan.FromSeconds(2)
                }
            });

            var eventBatch = await producerClient.CreateBatchAsync();

            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message))));
            await producerClient.SendAsync(eventBatch);
        }
    }
}
