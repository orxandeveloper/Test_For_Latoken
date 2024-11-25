using LAI.Application.IServices.ITelegramServices;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace LAI.Application.Worker
{
    public class ChatHandler : IChatHandler
    { 

        MessageWorker messageWorker;
        ITelegramBotClient botClient;
        IMessageErrorHandler messageErrorHandler;
        CancellationTokenSource cts;
        ReceiverOptions receiverOptions;

        public ChatHandler( 
                      ITelegramBotClient botClient,
                   ReceiverOptions receiverOptions,
                      IMessageErrorHandler messageErrorHandler,
                      MessageWorker messageWorker)
        { 
            this.botClient = botClient;
            this.cts = new CancellationTokenSource();
            this.receiverOptions = receiverOptions;
            this.messageErrorHandler = messageErrorHandler;
            this.messageWorker = messageWorker;
        }

        public void RunWorker()
        {
             
            botClient.StartReceiving(
               messageWorker.MessageHandler, 
                messageErrorHandler.HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token
            );
        }
    }
}
