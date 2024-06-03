using HackerNewsBestStories.Entities.Dtos;

namespace HackerNewsBestStories.Contracts
{
    public interface IHackerNewsBestStoryCacheService
    {
        int Count { get; }

        void AppendData(IList<HackerNewsBestStory> data);
        IList<HackerNewsBestStory> GetTop(int n);
        bool TryReset();
    }
}