using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace HackerNewsApiExercise.Models.Entities
{
    public class HackerNewsStory
    {
        /// <summary>
        /// Story item unique id.
        /// </summary>
        /// 
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// true if the item is deleted.
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// The type of item.One of "job", "story", "comment", "poll", or "pollopt".
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The username of the item's author.
        /// </summary>
        [JsonPropertyName("by")]
        public string By { get; set; }

        /// <summary>
        /// Creation date of the item, in Unix Time.
        /// </summary>
        [JsonPropertyName("time")]
        public string Time { get; set; }

        /// <summary>
        ///  The comment, story or poll text. HTML.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// The title of the story, poll or job. HTML.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// The comment's parent: either another comment or the relevant story.
        /// </summary>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

        /// <summary>
        ///  The pollopt's associated poll.
        /// </summary>
        [JsonPropertyName("poll")]
        public string Poll { get; set; }

        /// <summary>
        /// The ids of the item's comments, in ranked display order.
        /// </summary>        
        [JsonPropertyName("kids")]
        public List<string> Kids { get; set; }

        /// <summary>
        /// The URL of the story.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The story's score, or the votes for a pollopt.
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        ///  In the case of stories or polls, the total comment count.
        /// </summary>
        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }
    }
}
