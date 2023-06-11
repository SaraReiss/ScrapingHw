using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.ComponentModel;

namespace ScraperHw.Data
{
    public static class LakewoodScoopScraper
    {
        public static List<LakewoodScoopItem> Scrape()
        {
            var html = GetLakwoodScoopHtml();
            return ParseHtml(html);
        }

        private static string GetLakwoodScoopHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };
            using var client = new HttpClient(handler);
            
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");

            var url = $"https://thelakewoodscoop.com";
            var html = client.GetStringAsync(url).Result;
            return html;
        }

        private static List<LakewoodScoopItem> ParseHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

            var divs = document.QuerySelectorAll(".td_module_flex");
            var items = new List<LakewoodScoopItem>();
            foreach (var div in divs)
            {
                LakewoodScoopItem item = new();
                var titleElement = div.QuerySelector(".entry-title.td-module-title");
                if (titleElement != null)
                {
                    item.Title = titleElement.TextContent;
                }

                var textElement = div.QuerySelector(".td-excerpt");
                if (textElement != null)
                {
                    item.Text = textElement.TextContent;
                }

                var imageElement = div.QuerySelector(".entry-thumb.td-thumb-css");
                if (imageElement != null)
                {
                    item.Image = imageElement.Attributes["data-img-url"].Value;
                }

                var dateElement = div.QuerySelector(".entry-date");
                if (dateElement != null)
                {
                    item.Date = dateElement.TextContent;
                }


                var commentsElement = div.QuerySelector(".td-module-comments");
                if (commentsElement != null)
                {
                    item.Comments = commentsElement.TextContent;
                }

                var h3Tags = div.GetElementsByTagName("h3");
                if (h3Tags != null)
                {
                    var aTag = h3Tags[0].GetElementsByTagName("a").FirstOrDefault();
                    if (aTag != null && aTag.HasAttribute("href"))
                    {
                        item.Url = aTag.GetAttribute("href");
                    }
                }

                //var CommentsUrl = div.QuerySelector("a.td-module-comments");
                //if (CommentsUrl != null)
                //{
                //    item.CommentsUrl = CommentsUrl.Attributes["href"].Value;
                //}



                items.Add(item);
            }

            return items;
        }
    }

}