using Newtonsoft.Json;

namespace Project.Source.Data
{
    public class OrderResponse
    {
        [JsonProperty("user_message")] 
        public string Message { get; set; }
    }
}