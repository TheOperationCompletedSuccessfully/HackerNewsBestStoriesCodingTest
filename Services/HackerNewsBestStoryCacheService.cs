using HackerNewsBestStories.Contracts;
using HackerNewsBestStories.Entities.Dtos;
using System.Collections.Concurrent;

namespace HackerNewsBestStories.Services
{
    public class HackerNewsBestStoryCacheService : IHackerNewsBestStoryCacheService
    {

        private readonly IConfiguration _configuration;
        private static readonly ConcurrentDictionary<int, HackerNewsBestStory> _cachedData = new ConcurrentDictionary<int, HackerNewsBestStory>();
        private DateTime _lastUpdate;

        public HackerNewsBestStoryCacheService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _lastUpdate = DateTime.UtcNow;
        }

        public int Count => _cachedData.Count;

        public bool TryReset()
        {
            var period = int.Parse(_configuration["HackerNewsCache:HackerNewsCacheResetPeriodInSeconds"] ?? "3600");
            if (DateTime.UtcNow > _lastUpdate.AddSeconds(period)) 
            {
                _cachedData.Clear();
                _lastUpdate = DateTime.UtcNow;
                return true;
            }
            return false;
        }

        public void AppendData(IList<HackerNewsBestStory> data)
        {
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = int.Parse(_configuration["HackerNewsParallelOptions:MaxDegreeOfParallelism"] ?? "4")
            };
            var oldCount = _cachedData.Count;
            Parallel.ForEach(Enumerable.Range(oldCount, data.Count), options, (item) => { _cachedData.AddOrUpdate(item, data[item- oldCount], (key, value) => value); });
        }

        public IList<HackerNewsBestStory> GetTop(int n)
        {
            return Enumerable.Range(0, n).Select(i => _cachedData[i]).ToList();
        }
    }
}
