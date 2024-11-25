namespace LAI.Domain.Models.GPTModels
{
    public class gptMessageRequest
    {
        public string role { get; set; } = "user";
        public string content { get; set; }

        public gptMessageRequest(string content)
        {
            this.content = content;
        }
    }
}
