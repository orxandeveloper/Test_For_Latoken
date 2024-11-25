namespace LAI.Domain.Models.GPTModels
{
    public class RequestBody
    {
        public string model { get; set; }
        public gptMessageRequest[] messages { get; set; }
    }
}
