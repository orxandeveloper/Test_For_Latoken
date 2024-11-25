using LAI.Domain.Models.GPTModels;

namespace LAI.Application.IServices.IGptServices
{
    public interface IGptServicesHandler
    {
        Task<string> SendRequestToOpenAIAsnc(string Message);


    }
}
