using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using HtmlWeb;
using HtmlAgilityPack;

namespace Crawler
{
    class Program
    {
        static List<string> urls = new List<string>();
        static List<string> phoneNumbers = new List<string>();
        static object lockObj = new object();

        static void Main(string[] args)
        {
            Console.Write("请输入关键字:");
            string keyword = Console.ReadLine();
            string[] searchUrls = new string[20];
            for (int i = 0; i < 20; i++)
            {
                searchUrls[i] = $"https://cn.bing.com/search?q={keyword}&first={i * 10}";
            }

            // 创建20个线程爬取数据
            Thread[] threads = new Thread[20];
            for (int i = 0; i < 20; i++)
            {
                threads[i] = new Thread(GetInfo);
                threads[i].Start(searchUrls[i]);
            }

            // 等待所有线程结束
            foreach (Thread thread in threads)
            {
                if (thread != null)
                    thread.Join();
            }

            // 显示结果
            Console.WriteLine($"总共爬取{phoneNumbers.Count}个高校电话号码:");
            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                Console.WriteLine($"{phoneNumbers[i]}  url:{urls[i]}");
            }

            Console.ReadKey();
        }

        static void GetInfo(object searchUrl)
        {
            string url = (string)searchUrl;
            HtmlWeb web = new HtmlWeb();
            try
            {
                HtmlDocument doc = web.Load(url);

                // 使用正则表达式寻找电话号码
                Regex regex = new Regex(@"\d{3,4}-\d{7,8}");
                MatchCollection matches = regex.Matches(doc.Text);

                // 获取页面链接     
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[starts-with(@href, 'http')]");

                for (int i = 0; i < matches.Count; i++)
                {
                    if (phoneNumbers.Count < 100)
                    {
                        string phoneNum = matches[i].Value.Trim();
                        if (!phoneNumbers.Contains(phoneNum))
                        {
                            lock (lockObj)
                            {
                                if (phoneNumbers.Count < 100 && !phoneNumbers.Contains(phoneNum))
                                {
                                    phoneNumbers.Add(phoneNum);
                                    if (i < nodes.Count)
                                        urls.Add(nodes[i].Attributes["href"].Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"爬取{url}时出错:{ex.Message}");
            }
        }
    }
}