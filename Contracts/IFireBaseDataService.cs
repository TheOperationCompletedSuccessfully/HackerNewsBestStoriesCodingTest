using System.Collections.Concurrent;

namespace HackerNewsBestStories.Contracts
{
    public interface IFireBaseDataService<TOutput> where TOutput : class, new()
    {
        void Parse(int item, HttpClient client, ConcurrentBag<TOutput> resultData);
    }
}