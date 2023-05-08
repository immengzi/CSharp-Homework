using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
namespace UniversityPhoneNumberCrawler
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();
        static readonly List<string> phoneNumberResults = new List<string>();
        static readonly object lockObj = new object();
        static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.GetEncoding("gb2312");
            Console.Write("请输入关键字：");
            string keyword = Console.ReadLine();
            Console.WriteLine($"搜索关键字：{keyword}");
            var searchResults = await Search(keyword);
            Console.WriteLine($"搜索结果：共 {searchResults.Count} 个");
            foreach (var result in searchResults)
            {
                Console.WriteLine($"  {result}");
            }
            Console.WriteLine("开始爬取电话号码...");
            var tasks = new List<Task>();
            foreach (var url in searchResults)
            {
                tasks.Add(Task.Run(() => CrawlUrl(url)));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"共爬取到 {phoneNumberResults.Count} 个电话号码：");
            foreach (var result in phoneNumberResults)
            {
                Console.WriteLine($"  {result}");
            }
        }
        static async Task<List<string>> Search(string keyword)
        {
            var searchUrls = new List<string>
            {
                // $"https://www.baidu.com/s?wd={keyword}",
                $"https://www.bing.com/search?q={HttpUtility.UrlEncode(keyword)}",
            };
            var results = new List<string>();
            foreach (var url in searchUrls)
            {
                Console.WriteLine($"正在访问 {url}...");
                try
                {
                    var html = await httpClient.GetStringAsync(url);
                    // 仅用于测试
                    // Console.WriteLine(html);
                    Console.WriteLine($"访问 {url} 成功，共 {html.Length} 字节");
                    var regex = new Regex(@"<a href=""(?<url>https?://[\w\d./?=#&]+)""");
                    var matches = regex.Matches(html);
                    foreach (Match match in matches)
                    {
                        var result = match.Groups["url"].Value;
                        results.Add(result);
                    }
                    Console.WriteLine($"从 {url} 中找到 {matches.Count} 个 URL");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"访问 {url} 失败：{ex.Message}");
                }
            }
            return results;
        }
        static void CrawlUrl(string url)
        {
            try
            {
                int phoneNumberCount = 0;
                Console.WriteLine($"正在访问 {url}...");
                var html = httpClient.GetStringAsync(url).Result;
                Console.WriteLine($"访问 {url} 成功，共 {html.Length} 字节");
                var regex = new Regex(@"\b(?:\d{2,3}-)?\d{8}\b");
                var matches = regex.Matches(html);
                Console.WriteLine($"从 {url} 中找到 {matches.Count} 个电话号码");
                foreach (Match match in matches)
                {
                    if (phoneNumberCount >= 100) break;
                    var phoneNumber = match.Value;
                    lock (lockObj)
                    {
                        if (!phoneNumberResults.Contains(phoneNumber))
                        {
                            phoneNumberResults.Add($"{phoneNumber}\t{url}");
                        }
                    }
                    phoneNumberCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"访问 URL {url} 失败：{ex.Message}");
            }
        }
    }
}