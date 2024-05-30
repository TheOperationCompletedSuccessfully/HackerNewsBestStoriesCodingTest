using System.Text.Json.Serialization;

namespace HackerNewsBestStories.Entities.Dtos
{
    public class HackerNewsFirebaseData
    {
        public int Id { get; set; } = 0;
        public int Descendants { get; set; }
        public string By { get; set; }
        public List<int> Kids { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("type")]
        public string StoryType { get; set; }
    }
}
