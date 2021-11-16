using Newtonsoft.Json;

namespace Project.Source.Data
{
    public class Item
    {
        public string Title { get; set; }
        [JsonProperty("item_id")] public string Id { get; set; }
        [JsonProperty("item_name")] public string ItemName { get; set; }
        [JsonProperty("item_image")] public string ItemImage { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        [JsonProperty("currency_sign")] public string CurrencySign { get; set; }
    }
}