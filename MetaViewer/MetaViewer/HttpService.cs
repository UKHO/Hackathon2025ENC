using HtmlAgilityPack;

namespace MetaViewer;

public class HttpService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    
    public async Task<string> GetDefinitionDataAsync(string input)
    {
        var url = $"https://www.teledynecaris.com/s-57/object/{input}.htm";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var temp = await response.Content.ReadAsStringAsync();

        var phrase = GetFirstPlainPTagContent(temp);

        return phrase;
    }
    
    private static string GetFirstPlainPTagContent(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var pTags = htmlDoc.DocumentNode.SelectNodes("//p");

        if (pTags != null)
        {
            foreach (var pTag in pTags)
            {
                if (!pTag.InnerHtml.Contains("<font", StringComparison.OrdinalIgnoreCase) &&
                    !pTag.InnerHtml.Contains("<b", StringComparison.OrdinalIgnoreCase) &&
                    !pTag.InnerHtml.Contains("<a", StringComparison.OrdinalIgnoreCase))
                {
                    return pTag.InnerText.Trim();
                }
            }
        }

        return null;
    }
}