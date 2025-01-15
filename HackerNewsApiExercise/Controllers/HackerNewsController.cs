using HackerNewsApiExercise.Clients.Contracts;
using HackerNewsApiExercise.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace HackerNewsApiExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly ILogger<HackerNewsController> _logger;

        private IHackerNewsService _hackerNewsService;

        public HackerNewsController(ILogger<HackerNewsController> logger,
                                    IHackerNewsService hackerNewsService)
        {
            _logger = logger;
            _hackerNewsService = hackerNewsService;
        }

        /// <summary>
        /// Get the best stories id's in HackerRank
        /// </summary>
        /// <returns> List of ids of best stories. </returns>
        [HttpGet("BestStories")]
        public async Task<IEnumerable<int>> GetBestStories()
        {
            return await _hackerNewsService.GetBestStoryIds();
        }

        /// <summary>
        /// Obtains a list of best stories depending on their score value in HackerRank.
        /// </summary>
        /// <param name="numberOfStories"> Number of best stories to obtain. </param>
        /// <returns> List of best Hacker News stories ordered in descending order by their score value. </returns>
        [HttpGet("BestStoriesByScoreDesc")]
        public async Task<IEnumerable<HackerNewsStory>> GetBestStoriesByScoreDesc(int? numberOfStories)
        {
            return await _hackerNewsService.GetNBestSotriesOrderedScoreDescending(numberOfStories);
        }

        /// <summary>
        /// Obtains a list of best stories depending on their score value in HackerRank.
        /// </summary>
        /// <param name="numberOfStories"> Number of best stories to obtain. </param>
        /// <returns> List of best Hacker News stories ordered in ascending order by their score value. </returns>
        [HttpGet("BestStoriesByScoreAsc")]
        public async Task<IEnumerable<HackerNewsStory>> GetBestStoriesByScoreAscending(int? numberOfStories)
        {
            return await _hackerNewsService.GetNBestSotriesOrderedScoreAscending(numberOfStories);
        }
    }
}
