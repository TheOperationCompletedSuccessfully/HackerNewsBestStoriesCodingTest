using AutoMapper;
using HackerNewsBestStories.Contracts;
using HackerNewsBestStories.Entities.Dtos;
using System.Collections.Concurrent;

namespace HackerNewsBestStories.Services
{
    public class FireBaseDataService<TOutput> : IFireBaseDataService<TOutput> where TOutput : class, new()
    {
        private readonly string _firebaseStoryUrlStringTemplate = "https://hacker-news.firebaseio.com/v0/item/{0}.json";
        private readonly IMapper _mapper;

        public FireBaseDataService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Parse(int item, HttpClient client, ConcurrentBag<TOutput> resultData)
        {
            var fireBaseData = client.GetFromJsonAsync<HackerNewsFirebaseData>(string.Format(_firebaseStoryUrlStringTemplate, item)).Result;
            if (fireBaseData != null)
            {
                resultData.Add(_mapper.Map<TOutput>(fireBaseData));
            }
        }

    }
}
