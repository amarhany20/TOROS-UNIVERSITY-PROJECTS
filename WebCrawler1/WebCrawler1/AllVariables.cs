using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WebCrawler1
{
    public class AllVariables //I wanted to make a mother class for all variables and inside it I will nest all types like the crawlingResults
    {
        public static  double tempUrlCount = 0;
        public static double CrawledUrlCount = 0;
        public static int makeSureCounter = 0;
        public static DispatcherTimer timer = new DispatcherTimer();
        public static bool blConitnue = false;
        public static string srPassedTime = null;
        public static bool blWorking = false;
        public static bool blPaused = false; 
        public static int irNumberOfTotalConcurrentCrawling = 20;
        public static object _lock_CrawlingSync = new object();
        public static bool blBeingProcessed = false;
        public static int irMaximumTryCounter = 3;
        public static List<Task> lstCrawlingTasks = new List<Task>();
        public static List<string> lstCurrentlyCrawlingUrls = new List<string>();
        public static object _lockSyncDiscoveredLinks = new object();
        public static object _lockDatabaseAdd = new object();
        public static int irDiscoveredUrlCount = 0;
        public static ObservableCollection<string> _Results = new ObservableCollection<string>();
        public static int irCrawledUrlCount = 0;
        public static DateTime dtStartDate;
        public static StreamWriter swLog = new StreamWriter("logs.txt", true, Encoding.UTF8);
        public static object _lock_swLogs = new object();

        public static double dblOldTime;
        public static ObservableCollection<string> UserLogs // PROP

        {
            get { return _Results; }
            set
            {
                _Results = value;
            }
        }
        public class CrawlingResults : CrawledUrlsTable //This is a nested class Under all Variables (In here I will store all of my Crawling Results)
                                                        //I will inherit this class with the tbl
        {
            public bool blCrawlSuccess = true;//Helping me in Methods and in saving to the db
            public static bool blCrawlExternalUrls = false;
            public static string MainUrlHost = null;
            public static bool blockCrawlingThisUrl = false;
            public CrawlingResults()//CONSTRUCTOR
            {
                LastCrawlingDate = new DateTime(1900, 01, 01);
                this.IsCrawled = false;
                CompressionPercent = 0;
                FetchedTime = 0;
                LinkDepthLevel = 0;
                CrawledPageTitle = null;
                SourceCode = null;
                UrlName = "";
                UrlHash = "";
                DiscoverDate = DateTime.Now;
                ParentUrlHash = "";
                TryCounter = 0;
                BlockThisUrl = false;
                Failed = false;
                OriginUrl = null;
                LastTryDate = new DateTime(1900, 01, 01);
            }
            public List<string> lstDiscoveredLinks = new List<string>();
        }
        public class MethodsVariables
        {

        }
        public class ErrorLogsVariables
        {
            public static StreamWriter SWErrorLogs = new StreamWriter("Error-Logs.txt", append: true, encoding: Encoding.UTF8);
            public static object _Lock_SwErrorLogs = new object();
        }

       
      
        public static class MainInformation 
        {
            public static string TotalTime = null;
            public static string TotalFoundUrls = null;
            public static string TotalCrawledUrls = null;
            public static string crawlSpeed = null;
            public static string urlFindingSpeed = null;
            public static string originalUrl = null;
            public static int failedUrls = 0;
            public static int totalBlocked = 0;

        }
    }
}
