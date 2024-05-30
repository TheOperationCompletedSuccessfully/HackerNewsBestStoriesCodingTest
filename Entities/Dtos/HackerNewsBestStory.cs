using System.Text.Json.Serialization;

namespace HackerNewsBestStories.Entities.Dtos
{
    public class HackerNewsBestStory
    {
        [JsonPropertyOrder(0)]
        public string Title { get; set; }

        [JsonPropertyOrder(1)]
        public string Uri { get; set; }

        [JsonPropertyOrder(3)]
        public string Time { get; set; }

        [JsonPropertyOrder(2)]
        public string PostedBy { get; set; }

        [JsonPropertyOrder(4)]
        public int Score { get; set; }

        [JsonPropertyOrder(5)]
        public int CommentCount { get; set; }
    }
}
