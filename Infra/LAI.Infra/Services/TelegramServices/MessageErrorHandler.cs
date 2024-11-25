using LAI.Application.IServices.ITelegramServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace LAI.Infra.Services.TelegramServices
{
    public class MessageErrorHandler : IMessageErrorHandler
    {
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string errorMessage = "";
            if (exception is ApiRequestException apiRequestException)
            {
                errorMessage = $"Telegram API Error:\nError Code: {apiRequestException.ErrorCode}\nMesaj: {apiRequestException.Message}";
            }
            else
            {
                errorMessage = $"An unknown error occurred:\n{exception}";
            }

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}
