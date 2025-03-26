using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using SimpleFeedReader;

namespace News
{
    public class NewsPlugin
    {
        [KernelFunction("get_news")]
        [Description("Gets the news stories for today.")]
        [return: Description("A list of the current news today.")]
        public async Task<List<FeedItem?>> GetNews(Kernel kernel, [Description("The category of news")] string category)
        {
            var reader = new FeedReader(true);
            var feedItems = await reader.RetrieveFeedAsync($"https://rss.nytimes.com/services/xml/rss/nyt/{category}.xml");
            return feedItems.ToList();
        }
    }
}
