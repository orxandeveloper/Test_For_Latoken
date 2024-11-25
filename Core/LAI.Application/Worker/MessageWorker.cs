using Telegram.Bot.Types;
using Telegram.Bot;
using LAI.Application.IServices.IGptServices;

namespace LAI.Application.Worker
{
    public class MessageWorker
    {
        IGptServicesHandler gpt;

        public MessageWorker(IGptServicesHandler gpt)
        {
            this.gpt = gpt;
        }

        public async Task MessageHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handled Message is :\"{update.Message.Text}\" ");
            string gptResponse="" ;
            while (true)
            {
                gptResponse = await gpt.SendRequestToOpenAIAsnc(update.Message.Text);
                if (gptResponse != null)
                {
                    Console.WriteLine($"Gpt Response: {gptResponse}");
                    break;
                }
            }

            await botClient.SendTextMessageAsync(
              chatId: update.Message.Chat.Id,
              text: gptResponse,
              cancellationToken: cancellationToken);
        }
    }
}
