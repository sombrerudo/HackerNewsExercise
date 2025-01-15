using HackerNewsApiExercise.Models.Clients.Contracts;
using HackerNewsApiExercise.Models.Entities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;

namespace HackerNewsApiExercise.Models.Clients
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private IMemoryCache _memoryCache;

        public HackerNewsService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Obtains list of best story id's in HackerRank API.
        /// </summary>
        /// <returns> List with id's of best stories. </returns>
        public async Task<List<int>?> GetBestStoryIds()
        {
            var bestStoryListIds = await _memoryCache.GetOrCreateAsync(
                                                "BestStoryListIds",
                                                async cacheEntry =>
                                                {
                                                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                                                    var uri = GetClientBaseUri("beststories");
                                                    var responseString = await _httpClient.GetStringAsync(uri);
                                                    var bestStoryIds = new List<int>();

                                                    if (!string.IsNullOrEmpty(responseString))
                                                    {
                                                        bestStoryIds = JsonConvert.DeserializeObject<List<int>>(responseString);
                                                    }

                                                    return bestStoryIds;
                                                });

            return bestStoryListIds;
        }

        /// <summary>
        /// Get best stories in Hacker News listed by descending Order throug Score value.
        /// </summary>
        /// <param name="numberOfStories"> Number of stories to obtain, </param>
        /// <returns> 
        /// List with the best stories in Hacker News listed by descending Order throug Score value. 
        /// <see cref=HackerNewsStory""/>
        /// </returns>
        public async Task<List<HackerNewsStory>?> GetNBestSotriesOrderedScoreDescending(int? numberOfStories)
        {
            var listOfBestStoriesIds = await this.GetBestStoryIds();

            if (listOfBestStoriesIds == null) throw new Exception("BestStories could not be retrieved.");

            var listHackerNewsSories = await GetListBestStories(listOfBestStoriesIds, numberOfStories);

            if (listHackerNewsSories != null)
            {
                listHackerNewsSories = listHackerNewsSories.OrderBy(x => x.Score).ToList();
            }

            if (numberOfStories != null)
            {
                listHackerNewsSories = listHackerNewsSories.Take(numberOfStories.Value).ToList();
            }

            return listHackerNewsSories;
        }

        /// <summary>
        /// Get best stories in Hacker News listed by ascending Order throug Score value.
        /// </summary>
        /// <param name="numberOfStories"> Number of stories to obtain, </param>
        /// <returns> 
        /// List with the best stories in Hacker News listed by ascending Order throug Score value. 
        /// <see cref=HackerNewsStory""/>
        /// </returns>
        public async Task<List<HackerNewsStory>?> GetNBestSotriesOrderedScoreAscending(int? numberOfStories)
        {
            var listOfBestStoriesIds = await this.GetBestStoryIds();

            if (listOfBestStoriesIds == null) throw new Exception("BestStories could not be retrieved.");

            var listHackerNewsSories = await GetListBestStories(listOfBestStoriesIds, numberOfStories);

            if (listHackerNewsSories != null)
            {
                listHackerNewsSories = listHackerNewsSories.OrderByDescending(x => x.Score).ToList();            
            }

            if (numberOfStories != null)
            {                
                listHackerNewsSories = listHackerNewsSories.Take(numberOfStories.Value).ToList();
            }

            return listHackerNewsSories;
        }

        /// <summary>
        /// Obtains a list of best stories published by HackerNews Service. First try to obtain the list from cache. 
        /// If it's not available it will get from the Hacher News API.
        /// </summary>
        /// <param name="listOfBestStoriesIds"> List with best stories in Hacker News. </param>
        /// <param name="numberOfStories"> Number of stories to obtain. </param>
        /// <returns> List with Hacker News best stories <seealso cref="HackerNewsStory"/> </returns>
        private async Task<List<HackerNewsStory>> GetListBestStories(List<int>? listOfBestStoriesIds, int? numberOfStories)
        {
            // Obtain the List from cache if is available.
            var hackerNewsList = await _memoryCache.GetOrCreateAsync(
            "BestStoryList",
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(5);
                List<HackerNewsStory> hackerNewsListFrromCache = new List<HackerNewsStory>();
           
                if (listOfBestStoriesIds != null)
                {
                    foreach (var storyId in listOfBestStoriesIds)
                    {
                        // Obtain a story from cache if it's available.
                        var hackerNewsStory = await _memoryCache.GetOrCreateAsync(
                                                    "BestStory-" + storyId,
                                                    async cacheEntry =>
                                                    {
                                                        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(15);
                                                        var uri = GetClientBaseUri("/item/" + storyId);
                                                        var responseString = await _httpClient.GetStringAsync(uri);
                                                        var hackerNewsStory = new HackerNewsStory();
           
                                                        if (!string.IsNullOrEmpty(responseString))
                                                        {
                                                            hackerNewsStory = JsonConvert.DeserializeObject<HackerNewsStory>(responseString);
           
                                                        }
           
                                                        return hackerNewsStory;
                                                    });

                        if (hackerNewsStory != null) hackerNewsListFrromCache.Add(hackerNewsStory);
                    }
                }

                return hackerNewsListFrromCache;           
            });

            return hackerNewsList;
        }

        private Uri GetClientBaseUri(string stringReplace)
        {
            // BaseAddress is configured in HttpClient configuration Program.cs 
            return new Uri(_httpClient.BaseAddress != null ?
                           _httpClient.BaseAddress.ToString() + stringReplace + ".json" :
                           "https://hacker-news.firebaseio.com/v0/" + stringReplace + ".json");
        }        
    }
}
