using HtmlAgilityPack;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoCronjob
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            /*Console.OutputEncoding = Encoding.UTF8;
            var source = "https://vnexpress.net/kinh-doanh";
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(source);
            var nodeList = doc.QuerySelectorAll("h3.title-news a");
            HashSet<string> setLink = new HashSet<string>();
            foreach (var node in nodeList)
            {
                var link = node.Attributes["href"].Value;
                if (string.IsNullOrEmpty(link))
                {
                    continue;
                }
                setLink.Add(link);
                Console.WriteLine(link.ToString());
            }*/
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myjob", "group1")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("mytrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(job, trigger);
            Console.ReadLine();
        }
    }
}
