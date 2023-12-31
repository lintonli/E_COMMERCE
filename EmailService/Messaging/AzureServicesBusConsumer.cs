﻿
using Azure.Messaging.ServiceBus;
using EmailService.Models.Dtos;
using EmailService.Service;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace EmailService.Messaging
{
    public class AzureServicesBusConsumer : IAzureBus
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusProcessor _mailProcessor;
        private readonly ServiceBusProcessor _orderServiceProcessor;
        private readonly MailsService _mailService;
        public AzureServicesBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue < string>("AzureConnectionString");
            _queueName = _configuration.GetValue<string>("QueuesAndTopics:RegisterQueue");

            var client = new ServiceBusClient(_connectionString);
            _mailProcessor = client.CreateProcessor(_queueName);
            _mailService = new MailsService(configuration);

        }
        public async Task start()
        {
            _mailProcessor.ProcessMessageAsync += OnRegisterUser;
            _mailProcessor.ProcessErrorAsync += ErrorHandler;
            await _mailProcessor.StartProcessingAsync();
        }
        public async Task stop()
        {
            await _mailProcessor.StopProcessingAsync();
            await _mailProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
          return Task.CompletedTask;
        }

        private async Task OnRegisterUser(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var user = JsonConvert.DeserializeObject<UserMessageDto>(body);
            try
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://t4.ftcdn.net/jpg/03/89/92/27/360_F_389922799_AZFyYZguDeEMoUdoROgEiJqfDPih1S12.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + user.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to our Website");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Start your First Adventure!!</p>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>Shop at lower rates!!</p>");
                await _mailService.sendEmail(user, stringBuilder.ToString());
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

       
    }
}
