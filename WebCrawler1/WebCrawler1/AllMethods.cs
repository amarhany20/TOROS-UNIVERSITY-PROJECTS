using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;

namespace WebCrawler1
{
    class AllMethods : AllVariables//ALL OF MY METHODS ARE HERE FOR CLEANER AND MORE SORTED CODING (OOB) ( I INHERITED ALL PUBLIC VARIABLES
                                        //it's Public to be accesed at MainWindow
                                        //It's static to be accessed directly through Class Name (Methods).
                                        //I didn't know why was static important but Ofcourse I understood it much better from your videos and this video is a very fast static summary (https://www.youtube.com/watch?v=86ymhq54V5k) 
                                        //(I used static before in my library systems Project but didn't know why)
                                        //Methods Start
    {

        public static void ClearDatabase() //Method 1 //This Clears the Database
        {
            using (var context = new DBWebCrawlerDatabase())
            {
                var ctx = ((IObjectContextAdapter)context).ObjectContext;
                ctx.ExecuteStoreCommand("Truncate table [CrawledUrlsTable]");//Truncate is the best way to clear the database.
            }

        }

        public static void Crawl(string strCrawlingUrl, int irDepthLevel, string strParentUrl, DateTime dtDiscoveredDate,int trycounter,DateTime dtLastTryDate ) //  (Most Important) It Crawl each link
        {
           
            var localVariable = strCrawlingUrl; //You said to be safe in case of Variable racing.
            AllVariables.CrawlingResults crawlResult = new AllVariables.CrawlingResults();//in here as you can see it enters the nested class within the class. //Init the Results
            crawlResult.UrlName = localVariable;//we added  url
            if (!string.IsNullOrEmpty(strParentUrl))
            {
                crawlResult.ParentUrlHash = strParentUrl;//we added parent urlhash
            }
            if (dtDiscoveredDate != DateTime.MinValue)
            {

                crawlResult.DiscoverDate = dtDiscoveredDate; //we added discovered date
            }
            if (dtLastTryDate != DateTime.MinValue)//we add the last try date
            {
                crawlResult.LastCrawlingDate = dtLastTryDate;
               
                var hourDiff = (DateTime.Now - dtLastTryDate).TotalHours; //we get hour diffrence as if it failed and more that 24 hours reset it and try again
                if (trycounter > 3 && hourDiff <= 24)
                {
                    crawlResult.blCrawlSuccess = false;//failed
                    crawlResult.Failed = true;//failed
                    MainInformation.failedUrls++;//failed
                    crawlResult.LastTryDate = DateTime.Now;
                }
                else if (trycounter > 3 && hourDiff > 24)//retry
                {
                    trycounter = 0;//reset 
                    crawlResult.Failed = false;//retry
                }
            }
           
            crawlResult.UrlHash = strCrawlingUrl.ComputeHashExt();//Normalizing Then SHA256 the url for the primary key
            


            //I added using system.Net.HTML for HTTP POST web request as you told us in Lecture 13 - 01:03:03 as you told us that it has better control and performance.

            //Web Request
            // Create a request for the URL.
           // WebRequest request = WebRequest.Create(
             // "https://docs.microsoft.com");
            // If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            //WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.


            //using (Stream dataStream = response.GetResponseStream())
            //{
            //    // Open the stream using a StreamReader for easy access.
            //    StreamReader reader = new StreamReader(dataStream);
            //    // Read the content.
            //    string responseFromServer = reader.ReadToEnd();
            //    // Display the content.
            //    Console.WriteLine(responseFromServer);
            //}
            //// Close the response.
            //response.Close();

            //Web Request End





            Stopwatch swCrawlerTimer = new Stopwatch();// For calculating MS
            swCrawlerTimer.Start(); //Starting the stop watch

            HtmlWeb webClient = new HtmlWeb(); //Init the webClient
            
            
            webClient.AutoDetectEncoding = true; //detecting the encoding
            webClient.BrowserTimeout = new TimeSpan(0, 2, 0);//Timeout time is 2 minutes
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();  //The url DOC init
            if (crawlResult.blCrawlSuccess == true)//if it's not failed from the try counter
            {
                try
                {
                    doc = webClient.Load(crawlResult.UrlName);//loading the doc by returning the url name
                    crawlResult.SourceCode = doc.Text; //take the source code of the url (THE DOC IS THE SOURCE COD)
                    crawlResult.CrawledPageTitle = Regex.Match(crawlResult.SourceCode, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
        RegexOptions.IgnoreCase).Groups["Title"].Value; 
                    crawlResult.LastTryDate = DateTime.Now;
                    crawlResult.LastCrawlingDate = DateTime.Now;//Last Date which is now
                }
                catch (Exception E)
                {
                    crawlResult.blCrawlSuccess = false;// in case there is an error .We disable extracting links and saving the discovered links in the db
                    ErrorLogs.LogError(E, "Crawl");//To check for errors after, It's method is down in a nested error logs class
                    crawlResult.LastTryDate = DateTime.Now;
                    trycounter++;
                }
                swCrawlerTimer.Stop();//We stopped the timer to calculate the time elabsed
                crawlResult.FetchedTime = Convert.ToInt32(swCrawlerTimer.ElapsedMilliseconds); //we added the time elabsed or the fetched time
            }
            crawlResult.TryCounter = (byte)trycounter;
            crawlResult.LinkDepthLevel = (short)irDepthLevel;
            SaveCrawlResultsInDatabase(crawlResult);//Saving the crawl Results Methods
            if (crawlResult.blCrawlSuccess == true)//if the crawl was successful
            {
                Interlocked.Increment(ref irCrawledUrlCount);// increasing the url count
                extractLinks(crawlResult, doc); //here we insert our crawlResults and the source code (DOC)
                 // we got the list of the discovered links to crawl after that
                
                saveDiscoveredLinksInDatabaseForFutureCrawling(crawlResult);

            }

            doc = null;//For Reseting


        }

       
        
      
        private static void extractLinks(CrawlingResults crCrawlingResult, HtmlDocument doc)//the url and the document of the html (The source which we will get everything from inside) we used this to get the string lists and the page title;
        {
            
            var baseUrl = new Uri(crCrawlingResult.UrlName);  //URI stands for Universal Resource Identifier to represents URIs such as "https://www.dotnetperls.com/". You can manage URI data directly with strings. (What is URI)  
            //Extracting all Links
            var vrNodes = doc.DocumentNode.SelectNodes("//a[@href]");//To select all <a> which contains all the URLS in HTML

            if (vrNodes != null) //Because foreach doesn't check nulls 
            {
                foreach (HtmlNode link in vrNodes) //I think this @href looks like the parameterized sql but I am not sure.
                {
                    HtmlAttribute attribute = link.Attributes["href"];//we get the attribute which is the other links to crawl them too.
                   var vrabsoluteUri = new Uri(baseUrl, attribute.Value.ToString().decodeUrl());
                    if (!vrabsoluteUri.ToString().StartsWith("http://") && !vrabsoluteUri.ToString().StartsWith("https://"))//We only take links with http/https
                    {

                        continue;//that means it failed the if condition so I am only breaking one instance in If condition or skipping one time.
                    }
                    else
                    {
                        crCrawlingResult.lstDiscoveredLinks.Add(vrabsoluteUri.ToString().Split('#').FirstOrDefault());//there is hashtage we need to get the first value only
                        //In here we added any found ulrs that start with http and https.
                    }

                }
            }
            crCrawlingResult.lstDiscoveredLinks = crCrawlingResult.lstDiscoveredLinks.Distinct().Where(pr=> pr.Length <201).ToList(); //  to remove duplicates
            var vrDocTitle = doc.DocumentNode.SelectSingleNode("//title")?.InnerHtml.ToString().Trim();//Getting the title out from the HTML <TITLE>
            vrDocTitle = WebUtility.HtmlDecode(vrDocTitle);//Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.
            crCrawlingResult.CrawledPageTitle = vrDocTitle;
        }
        private static void SaveCrawlResultsInDatabase(AllVariables.CrawlingResults crawlingResults)//Saving to SQL database Method 
        {
            
            lock (_lockDatabaseAdd)
            {
                using (var context = new DBWebCrawlerDatabase())//The crawlingResults before adding the missing here contains the 1. URL , 2. ParentURLHASH , 3. URLHASH , 4. FetchedTime , 5. Page Title , 6. Discovered Date , 7. LastCrawlDate(Constructor) , 8. DiscoveredDate (When Button is clicked) ,   

                //In here we need to add the 1. Source Code (Compressed) , 2. Compression Percent , 3.isCrawled? , 4.HostUrl , 5.TryCounter
                {

                    var vrResult = context.CrawledUrlsTables.SingleOrDefault(b => b.UrlHash == crawlingResults.UrlHash);//We check if it exsists in the db  (b.URlHash == crawling.UrlHash)
                    

                    crawlingResults.HostUrl = crawlingResults.UrlName.returnRootUrl();//we added the Host URl
                    crawlingResults.OriginUrl = MainInformation.originalUrl.Normalize();
                    if (crawlingResults.blCrawlSuccess == true)
                    {
                        crawlingResults.IsCrawled = true;
                        if (!string.IsNullOrEmpty(crawlingResults.SourceCode))
                        {
                            double dblTempSourceCodeOrigin = crawlingResults.SourceCode.Length.ToDouble();//Temp Double to check compression
                            crawlingResults.SourceCode = crawlingResults.SourceCode.CompressString();//add the compressed source code
                            crawlingResults.CompressionPercent = Convert.ToByte(Math.Floor(StringCompressor.CompressionCalculator.CompressionPercent(dblTempSourceCodeOrigin, crawlingResults.SourceCode.Length.ToDouble())));//Calculating Percent of the compression
                        }
                    }

                    CrawledUrlsTable finalResult = crawlingResults.ConverToBaseMainUrlClass();//I Searched everywhere for this, I didn't understand Why did we use it but here is what I know: we will use copy properties of object to another object without changing reference because if I delete from the server then re add it would be heavy. I will always use it before inserting to db 

                    if (vrResult != null)//THat means it exitst before (We used to delete then add now we will only copy
                    {

                        //context.CrawledUrlsTables.Remove(vrResult);
                        //context.SaveChanges();

                        finalResult.DiscoverDate = vrResult.DiscoverDate;//we add the old discoverdate
                        finalResult.LinkDepthLevel = vrResult.LinkDepthLevel;//we add the main currentDepthLevel
                        finalResult.TryCounter = crawlingResults.TryCounter; //we add the changed tryCounter from above  
                        finalResult.LastTryDate = crawlingResults.LastTryDate; //we add the old or changed upon crawl
                        finalResult.Failed = crawlingResults.Failed; //we add the new status
                        finalResult.CopyProperties(vrResult);//WE Copied the prop

                    }
                    else
                    {
                        context.CrawledUrlsTables.Add(finalResult);//we add a new row  because this urlhash doesnt exist .
                    }
                    try
                    {
                        var done = context.SaveChanges();// Saving changes to DB
                    }
                    catch (Exception)
                    {
                        AllVariables.MainInformation.failedUrls++;
                    }
                        
                    
                   
                   
                    


                }


            }
        }

        private static void saveDiscoveredLinksInDatabaseForFutureCrawling(CrawlingResults myCrawlingResults)
        {
            lock (_lockSyncDiscoveredLinks)//Locking for sync safe
            {

                using (var context = new DBWebCrawlerDatabase())//context to access with the dp 
                {
                    HashSet<string> hsProcessedUrls = new HashSet<string>();
                    foreach (var vrPerLink in myCrawlingResults.lstDiscoveredLinks)//foreach Link
                    {
                        var vrHashedLink = vrPerLink.ComputeHashExt();//I add the hashed link to var
                        if (hsProcessedUrls.Contains(vrHashedLink))
                        {
                            //if it already exsists Continute
                            continue;
                        }
                        var vrResult = context.CrawledUrlsTables.Any(dbRecord => dbRecord.UrlHash == vrHashedLink);//IN Here when I check it using sql profile trace and take the code and write it in sql query if it's index seek then it's good for performance and it's not good if it's index scan 
                        //In here I check if it exists or not
                        if (vrResult == false)//if it doesnt exist
                        {
                            CrawlingResults newLinkCrawlingResult = new CrawlingResults();
                            
                            newLinkCrawlingResult.UrlName = EncAndNorm.normalizingUrl(vrPerLink) ;//URL adding
                            newLinkCrawlingResult.HostUrl = newLinkCrawlingResult.UrlName.returnRootUrl();//HostUrl Adding
                            newLinkCrawlingResult.UrlHash = vrHashedLink;//Url Hash Adding
                            newLinkCrawlingResult.ParentUrlHash = myCrawlingResults.UrlHash;//ParentalUrlAdding
                            newLinkCrawlingResult.LinkDepthLevel = (short)(myCrawlingResults.LinkDepthLevel + 1);//LinkDepth + 1 because these urls are inside another url
                            if (!AllVariables.CrawlingResults.blCrawlExternalUrls)
                            {
                                if (!string.Equals(EncAndNorm.normalizingUrl(CrawlingResults.MainUrlHost) , EncAndNorm.normalizingUrl(vrPerLink).returnRootUrl()))
                                {
                                    newLinkCrawlingResult.BlockThisUrl = true;
                                    AllVariables.MainInformation.totalBlocked++;
                                }
                                else
                                {
                                    newLinkCrawlingResult.BlockThisUrl = false;//Useless
                                }
                            }
                            newLinkCrawlingResult.OriginUrl = MainInformation.originalUrl.Normalize();
                            context.CrawledUrlsTables.Add(newLinkCrawlingResult.ConverToBaseMainUrlClass());//we add that row
                            hsProcessedUrls.Add(vrHashedLink);//?
                            Interlocked.Increment(ref irDiscoveredUrlCount);//increasing the urlcount

                        }
                    }
                    context.SaveChanges();//SAVING
                }
            }
           
        }



    }
    public class ErrorLogs : AllVariables.ErrorLogsVariables  //Inheritance to inherit variables from all variable class because I set all variables there and all class here (Don't know if it's effiecnt or not but I feel that they are better sorted this way.)
    {
        static void FlushingSW()
        {
            SWErrorLogs.AutoFlush = true;
        }
        public static void LogError(Exception E, string CallingMethodName)
        {
            lock (_Lock_SwErrorLogs)//Lock Methodology to synchronize access to a non-thread safe object streamwriter (You gave us this in lecture 13 -  1:06:41)
            {
                SWErrorLogs.WriteLine($"Error Happened in: {CallingMethodName}.\t Time : {DateTime.Now} \nThe Error : \n {E.Message} \n The inner Exception : \n {E?.InnerException?.Message} \n The StackTrace \n {E?.InnerException?.StackTrace} \n \n End Of Error \n\n");
            }
        }

    }
}
