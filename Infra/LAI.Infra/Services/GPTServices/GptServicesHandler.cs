using LAI.Application.IServices.IGptServices;
using LAI.Domain.Models.GPTModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;

namespace LAI.Infra.Services.GPTServices
{
    public class GptServicesHandler : IGptServicesHandler
    {
        GptRequestModel gptRequest;
        RequestBody requestBody;
       // HttpClient client;
        public GptServicesHandler(IOptions<GptRequestModel> gptRequest, IOptions<RequestBody> requestBody
           // HttpClient client
            )
        {
            this.gptRequest = gptRequest.Value;
            this.requestBody = requestBody.Value;
            //this.client = client;
        }

        public async Task<string> SendRequestToOpenAIAsnc( string Message )
        { 
            requestBody.messages = new gptMessageRequest[] { new gptMessageRequest(Message) };

            using (HttpClient client=new HttpClient())
            {
               
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {gptRequest.apiKey}");

                
                string jsonRequestBody = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                try
                {
                   
                    HttpResponseMessage response = await client.PostAsync(gptRequest.endPoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                         
                        string responseString = await response.Content.ReadAsStringAsync();

                        
                        using (JsonDocument doc = JsonDocument.Parse(responseString))
                        {
                            var choices = doc.RootElement.GetProperty("choices");
                            string reply = choices[0].GetProperty("message").GetProperty("content").GetString();
                            return reply;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
