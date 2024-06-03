using AutoMapper;
using HackerNewsBestStories.Contracts;
using HackerNewsBestStories.Entities.Dtos;
using HackerNewsBestStories.Entities.Extended;
using HackerNewsBestStories.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace HackerNewsBestStories.Controllers
{
    [ApiController]
    [Route("[controller]/api/v0")]
    public class HackerNewsBestStoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HackerNewsBestStoryController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFireBaseDataService<HackerNewsBestStory> _fireBaseDataService;
        private readonly IHackerNewsBestStoryCacheService _hackerNewsBestStoryCacheService;
        private const string bestStoriesUrlString = "https://hacker-news.firebaseio.com/v0/beststories.json";
        private static object lockObj = new object();

        public HackerNewsBestStoryController(IMapper mapper, ILogger<HackerNewsBestStoryController> logger, 
            IConfiguration configuration, IFireBaseDataService<HackerNewsBestStory> fireBaseDataService,
            IHackerNewsBestStoryCacheService hackerNewsBestStoryCacheService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _fireBaseDataService = fireBaseDataService ?? throw new ArgumentNullException(nameof(fireBaseDataService));
            _hackerNewsBestStoryCacheService = hackerNewsBestStoryCacheService ?? throw new ArgumentNullException(nameof(hackerNewsBestStoryCacheService));
        }

        [HttpGet("takefirst/{n}")]
        [ResponseCache(Duration = 3600)]
        public JsonResult Get(int n)
        {
            if(n<=0 || n>1000)
            {
                var result = new JsonResult(new List<HackerNewsBestStory>())
                {
                    StatusCode = 400
                };
                return result;
            }

            var resultData = new ConcurrentBag<HackerNewsBestStory>();
            var data = new List<int>();
            var cached = _hackerNewsBestStoryCacheService.Count;
            if (_hackerNewsBestStoryCacheService.TryReset() || n > cached)
            {
                lock (lockObj)
                {
                    try
                    {

                        var toTake = n - cached;
                        using (var client = new HackerNewsClient(_configuration))
                        {
                            data = client.GetFromJsonAsync<IEnumerable<int>>(bestStoriesUrlString).Result?.Skip(cached).Take(toTake).ToList() ?? [];
                            if (data != null)
                            {
                                var options = new ParallelOptions
                                {
                                    MaxDegreeOfParallelism = int.Parse(_configuration["HackerNewsParallelOptions:MaxDegreeOfParallelism"] ?? "4")
                                };
                                Parallel.ForEach(data, options, item => { _fireBaseDataService.Parse(item, client, resultData); });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical($"Error happened in HackerNewsBestStoryController class, method Get, parameter n = {n}", ex);
                    }
                    var finalResult = resultData.OrderByDescending(item => item.Score).ToList();
                    _hackerNewsBestStoryCacheService.AppendData(finalResult);
                }
            }

            return new JsonResult(_hackerNewsBestStoryCacheService.GetTop(n));
        }
    }
}
