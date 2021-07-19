using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using EventDrivenArchitectureExample.Data.Messages;
using EventDrivenArchitectureExample.Stock.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Stock.HostedService
{
    public class ProcessStockCompensationConsumer : BackgroundService
    {
        private readonly IStockHandler _stockHandler;

        public ProcessStockCompensationConsumer(IServiceProvider serviceProvider)
        {
            _stockHandler = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IStockHandler>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var eventHubConnectionString = "<your eventhub connection>";
            var eventHubName = "stock-compensation";

            var blobStorageConnectionString = "<your blob storage connection>";
            var stockContainer = "<container name>";

            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, stockContainer);
            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, eventHubConnectionString, eventHubName);

            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            await processor.StartProcessingAsync();
        }

        public async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            var orderMessage = JsonSerializer.Deserialize<OrderCreatedMessage>(eventArgs.Data.Body.ToArray());

            Console.WriteLine("\tReceived event: {0}", orderMessage);

            await _stockHandler.Handle(orderMessage);

            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        public static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
