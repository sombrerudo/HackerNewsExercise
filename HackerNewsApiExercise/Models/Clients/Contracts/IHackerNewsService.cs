using HackerNewsApiExercise.Models.Entities;

namespace HackerNewsApiExercise.Models.Clients.Contracts
{
    public interface IHackerNewsService
    {
        /// <summary>
        /// Get the best stories id's in HackerRank
        /// </summary>
        /// <returns> List of ids of best stories. </returns>
        Task<List<int>?> GetBestStoryIds();


        /// <summary>
        /// Obtains a list of best stories depending on their score value in HackerRank.
        /// </summary>
        /// <param name="numberOfStories"> Number of best stories to obtain. </param>
        /// <returns> List of best Hacker News stories ordered in descending order by their score value. </returns>
        Task<List<HackerNewsStory>?> GetNBestSotriesOrderedScoreDescending(int? numberOfStories);


        /// <summary>
        /// Obtains a list of best stories depending on their score value in HackerRank.
        /// </summary>
        /// <param name="numberOfStories"> Number of best stories to obtain. </param>
        /// <returns> List of best Hacker News stories ordered in ascending order by their score value. </returns>
        Task<List<HackerNewsStory>?> GetNBestSotriesOrderedScoreAscending(int? numberOfStories);

    }
}
