// See https://aka.ms/new-console-template for more information


using LAI.Application.IServices.ITelegramServices;
using LAI.Application.Worker;
using LAI.Infra.Services.TelegramServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;
using LAI.Application.IServices.IGptServices;
using LAI.Infra.Services.GPTServices;
using Microsoft.Extensions.Configuration;
using LAI.Domain.Models.GPTModels;

class Program
{
    static async Task Main(string[] args)
    {


        var host = CreateHostBuilder(args).Build();
        var worker = host.Services.GetRequiredService<IChatHandler>();
        worker.RunWorker();
        await host.RunAsync();

    }
    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
         .ConfigureHostConfiguration(hostConfing =>
         {

             hostConfing.SetBasePath(Directory.GetCurrentDirectory());
             hostConfing.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
             hostConfing.Build();
         })

        .ConfigureLogging(logging =>
        { 
            logging.ClearProviders();
            logging.AddConsole();
        })
            .ConfigureServices((context, services) =>
            {
                
                services.Configure<GptRequestModel>(context.Configuration.GetSection("GptRequestModel"));
                services.Configure<RequestBody>(context.Configuration.GetSection("RequestBody"));

                var appSettings = context.Configuration.GetSection("AppSettings");
               // services.AddHttpClient();

                string telegramToken = appSettings["TelegramToken"]; 
                services.AddTransient<IMessageErrorHandler, MessageErrorHandler>(); 
                services.AddTransient< MessageWorker>();
                services.AddSingleton(new ReceiverOptions
                {
                    AllowedUpdates = Array.Empty<UpdateType>() 
                });
                services.AddTransient<ITelegramBotClient>(provider =>
                {
                  
                    return new TelegramBotClient(telegramToken);
                });
                services.AddTransient<IChatHandler, ChatHandler>();
                services.AddTransient<IGptServicesHandler, GptServicesHandler>();
                
            });
}