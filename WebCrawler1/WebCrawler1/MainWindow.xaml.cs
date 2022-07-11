using System;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WebCrawler1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThreadPool.SetMaxThreads(100000, 100000);//you told us better performance
            ThreadPool.SetMinThreads(100000, 100000);//you told us better performance
            ServicePointManager.DefaultConnectionLimit = 1000;
            listbox_Results.ItemsSource = AllVariables.UserLogs;//Getting the userlogs to the listbox
            label_Status.Content = "Current Status: Waiting For Input";//status
            if (File.Exists("SaveSession.txt"))//it's checking if the file exists, if it exists then it will show a messege box do you want to continue or not
            {
                if (MessageBox.Show("Unended Crawl detected do you want to complete it","confirmation",MessageBoxButton.YesNo)==MessageBoxResult.Yes)//it's checking if the file exists, if it exists then it will show a messege box do you want to continue or not
                {
                    loadLastSession();//if yes load last session
                }
            }
            dgUpdate();//updating db
        }

        private void btn_Crawl_Click(object sender, RoutedEventArgs e)//Main CrawlPage Click 
        {
            string txt = textbox_Url.Text;//I am taking it to a temp variable because I am calling the crawl in a task and I don't want to use the main ui thread
            if (textbox_Url.Text.Trim() == "")//Detecting if it's empty
            {
                MessageBox.Show("Please enter a Url to crawl");
                return;
            }
            reset();//Reseting Everything to start the new crawl
            File.Delete("SaveSession.txt");//Deleting the old session
            label_Status.Content = "Current Status: Started Working (Getting First URL)";//Info for the use
            AllVariables.dblOldTime = 0;//Reset the old time as it's a new crawl
            AllVariables.dtStartDate = DateTime.Now; // getting current dateTime as starting date for timers
            AllVariables.CrawlingResults.blCrawlExternalUrls = checkbox_CrawlExternalUrls.IsChecked == true;//cheching if it's checked in a public variable
            AllVariables.CrawlingResults.MainUrlHost = textbox_Url.Text.returnRootUrl();//Getting the main url host to block external urls
            AllVariables.MainInformation.originalUrl = textbox_Url.Text;//ORIGINAL URL 
            AllVariables.blWorking = true;//Bool Working to make the program know it's working now
            AllVariables.blPaused = false;//To reset paused cause it started working now

            Task task = Task.Run(() => AllMethods.Crawl(txt, 0, null, DateTime.Now, 0, DateTime.MinValue));//Calling it inside a task to make ui responsive as it will work in a background thread

            for (int i = 0; i < 10; i++)//For user Logs to know that we are starting a new crawl
            {

                AllVariables.UserLogs.Insert(0, "Starting a new crawl");
            }
            CheckingTimer();//Activating the dispatcher Timer


        }
        private void btn_PauseCrawl_Click(object sender, RoutedEventArgs e)//Pause when it's working
        {
            if (AllVariables.blPaused == true)//If it's paused block it
            {
                MessageBox.Show("it's already Paused");
                return;
            }
            if (AllVariables.blWorking == false)//if it's not working block it
            {
                MessageBox.Show("Start A new crawl");
                return;
            }
            AllVariables.blPaused = true;//activating pause

            AllVariables.dblOldTime = AllVariables.srPassedTime.ToDouble();//taking the old time
            AllVariables.timer.Stop();//Stop the dispatcher Timer
            label_Status.Content = "Current Status: Paused"; // User Status

        }
        private void btn_ContinueCrawl_Click(object sender, RoutedEventArgs e)//Continue when it's paused
        {
            if (AllVariables.blPaused)//Checking if it's paused
            {
                AllVariables.dtStartDate = DateTime.Now; //new starting date (I saved the old date in a public static variable)
                AllVariables.blConitnue = true;//Continuing (It's code is in startPoiling
                AllVariables.blPaused = false;//Pausing is false
                AllVariables.timer.Start();////Starting the dispatcher Timer
                return;

            }
            if (AllVariables.blPaused == false && AllVariables.blWorking == true)//checking if it's not paused
            {
                MessageBox.Show("It's Already working");

                return;
            }
            if (AllVariables.blPaused == false && AllVariables.blWorking == false)//it's not working
            {
                MessageBox.Show("Please start a new Url crawl");
            }


        }
        private void btn_StopCrawl_Click(object sender, RoutedEventArgs e)//Stopping crawl and resettubg
        {
            if (!AllVariables.blWorking)//it's not working
            {
                MessageBox.Show("It's already Stopped");
                return;
            }
            if (MessageBox.Show("Are you sure? This will end this crawl and it will reset everything", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)//checking if he is sure or not
            {
                label_Status.Content = "Current Status: Stopping";//status
                AllVariables.timer.Stop();//stopping
                reset();//reseting
                textbox_Url.Text = "";//emptying textbox
                Thread.Sleep(500);//I don't know why I used this but I think I wanted to give it time untill it finishes tasks maybe
                label_Status.Content = "Current Status: Waiting For Input";//status
                AllVariables.blWorking = false;//it's not working
            }
        }
        private void btn_RefreshDatabase_Click(object sender, RoutedEventArgs e)//Refreshing DB
        {
            dgUpdate();
        }
        private void button_Set_Click(object sender, RoutedEventArgs e)//setting tasks or threads ammount (Speed Diffrence is obvious)
        {
            try
            {
                AllVariables.irNumberOfTotalConcurrentCrawling = Int32.Parse(textbox_Threads.Text.Trim());//setting tasks or threads ammount
            }
            catch (Exception)
            {
                MessageBox.Show("Make Sure you wrote the number correctly");
                return;
            }
            // Task
            //.Factory.StartNew(() => {

            //}).Wait();



        }
        private void btn_ContinueOld_Click(object sender, RoutedEventArgs e)//Continue old session
        {
            loadLastSession();//Loading old session

        }
        private void button_DeleteDB_Click(object sender, RoutedEventArgs e)//truncate the tables from the db
        {
            if (AllVariables.blWorking == true)//if it's working block it to prevent errors (Only works when stopped)
            {
                MessageBox.Show("Can't Delete while Working");
                return;
            }
            Task.Run(() =>
            {

                AllMethods.ClearDatabase();//clear db methods
                MessageBox.Show("Database Deleted");
                dgUpdate();//updating db
            });//running it in a task the to make ui responsive
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)//blocking words in threads text box
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//writing info to txt to read it when continuing last session
        {
            if (AllVariables.timer.IsEnabled)
            {
                StreamWriter file = new StreamWriter("SaveSession.txt", append: false, encoding: Encoding.UTF8);

                file.WriteLine("TotalTime:" + AllVariables.MainInformation.TotalTime);//writing the total time
                file.WriteLine("TotalCrawledUrls:" + AllVariables.MainInformation.TotalCrawledUrls);//writng the total crawled urls
                file.WriteLine("TotalFoundUrls:" + AllVariables.MainInformation.TotalFoundUrls);//writing 
                file.WriteLine("urlFindingSpeed:" + AllVariables.MainInformation.urlFindingSpeed);
                file.WriteLine("crawlSpeed:" + AllVariables.MainInformation.crawlSpeed);
                file.WriteLine("failedUrls:" + AllVariables.MainInformation.failedUrls);
                file.WriteLine("totalBlocked:" + AllVariables.MainInformation.totalBlocked);
                file.WriteLine("" + AllVariables.MainInformation.originalUrl);
                file.WriteLine(checkbox_CrawlExternalUrls.IsChecked==true);
                file.Flush();
                file.Close();
            }
            else
            {
                File.Delete("SaveSession.txt");//This is useless
            }


        }
        public void loadLastSession()//Loading the last session information from a txt file
        {
            if (File.Exists("SaveSession.txt"))
            {
                label_Status.Content = "Current Status: Getting Info";//Getting info 
                string text = File.ReadAllText("SaveSession.txt"); // reading the text
                string[] lstOldSession = Regex.Split(text, "\r\n|\r|\n"); //Putting it in a string array
                AllVariables.MainInformation.TotalTime = lstOldSession[0].Split(':')[1]; // getting the total old time value
                AllVariables.dblOldTime = AllVariables.MainInformation.TotalTime.ToDouble(); // adding the total old time to old time
                AllVariables.MainInformation.TotalCrawledUrls = lstOldSession[1].Split(':')[1];//getting the total crawledUrl numbers
                AllVariables.MainInformation.TotalFoundUrls = lstOldSession[2].Split(':')[1];//getting the total FoundUrl numbers
                AllVariables.MainInformation.urlFindingSpeed = lstOldSession[3].Split(':')[1];//getting the discovery speed
                AllVariables.MainInformation.crawlSpeed = lstOldSession[4].Split(':')[1];//getting the crawl speed
                AllVariables.MainInformation.failedUrls = int.Parse(lstOldSession[5].Split(':')[1]);//getting the total failedurl numbers
                AllVariables.MainInformation.totalBlocked = int.Parse(lstOldSession[6].Split(':')[1]);//getting the total blockedurl numbers
                AllVariables.MainInformation.originalUrl = lstOldSession[7];//getting the origin url for insertion to textbox and origin url in database numbers
                try
                {
                    AllVariables.irCrawledUrlCount = Int32.Parse(AllVariables.MainInformation.TotalCrawledUrls);
                    AllVariables.irDiscoveredUrlCount = Int32.Parse(AllVariables.MainInformation.TotalFoundUrls);
                }
                catch (Exception)
                {
                    MessageBox.Show("error in old file");
                    File.Delete("SaveSession.txt");
                }
                
                checkbox_CrawlExternalUrls.IsChecked = bool.Parse(lstOldSession[8]);//getting whether the crawlexternal url is checked or not
                AllVariables.dtStartDate = DateTime.Now; // setting the start time now
                AllVariables.CrawlingResults.blCrawlExternalUrls = checkbox_CrawlExternalUrls.IsChecked == true; //checking or not checking
                textbox_Url.Text = AllVariables.MainInformation.originalUrl;//writing the origin url in the textbox
                AllVariables.CrawlingResults.MainUrlHost = textbox_Url.Text.returnRootUrl();//getting the main url host back again.
                CheckingTimer();//Starting the dispatch timer
                AllVariables.blWorking = true;//setting it as working
                File.Delete("SaveSession.txt"); // deleting the old session
            }
            else
            {
                MessageBox.Show("Couldn't Find old session.");//if it doesn't exist
                return;
            }
        }
        private void dgUpdate()//updating the datagrid
        {
            using (var context = new DBWebCrawlerDatabase())
            {
                this.Dispatcher.Invoke(() =>
                {
                    dg_CrawledUrlsTable.ItemsSource = context.CrawledUrlsTables.ToList();//I used dispatcher because it works in the ui 
                });
                
            }
           

           
        }
        public void updatingInfo()//Updating the info to labels from public static variables
        {


            label_RunTime.Content = "Run Time: " + AllVariables.MainInformation.TotalTime +" Minute/s";
            label_NumbersOfUrlFound.Content = "Number of Urls Found: " + AllVariables.MainInformation.TotalFoundUrls;
            label_NumbersOfUrlsCrawled.Content = "Number of Urls Crawled: " + AllVariables.MainInformation.TotalCrawledUrls;
            label_UrlsFoundPerMinute.Content = "Urls Found Per Minute: " + AllVariables.MainInformation.urlFindingSpeed;
            label_UrlsCrawledPerMinute.Content = "Urls Crawled Per Minute: " + AllVariables.MainInformation.crawlSpeed;
            label_FailedUrlsCount.Content = "Failed Crawling Urls Count: " + AllVariables.MainInformation.failedUrls;
            label_Status.Content = "Current Status: Working";
            label_UrlCrawlBlockCount.Content = "Url Crawling Block Count: " + AllVariables.MainInformation.totalBlocked;
            label_CurrentUsedThreads.Content = "Current Used Threads: " + Process.GetCurrentProcess().Threads.Count;
        }
        private async void OnTimerTick(object sender, EventArgs e)//I used this to make ui responsive
        {
            
            await Task.Run(()=> StartPollingAwaitingUrls());//I used await 

            // probably update UI after await
        }
        public void CheckingTimer()//starting the dispatcher timer
        {
            AllVariables.timer.Tick += new EventHandler(OnTimerTick);//I called the on timer tick for async and await
            AllVariables.timer.Interval = new TimeSpan(0, 0, 0, 0, 500);//every 500ms or 0.5 second there is a tick 
            AllVariables.timer.Start();//startting the dispatcher
        }
        public void StartPollingAwaitingUrls()//In here we update and we do everything (We get the non crawled urls and crawl them and we get the error urls and we try to crawl them after 24hrs) (We update the user logs) (We update the status) 
        {
            //I used delegate to make it ui responsive and because it can't update user logs without it as without delegate we are trying to take update the userlogs and the ui items in another thread
            App.Current.Dispatcher.Invoke((Action)delegate
            {
            lock (AllVariables.UserLogs)//LOCKING FOR PREVENTING RACING 
            {
                    if (AllVariables.blConitnue)
                    {
                        //AllVariables.tempUrlCount = AllVariables.irDiscoveredUrlCount.ToDouble();//Useless as I deleted it
                        //AllVariables.CrawledUrlCount = AllVariables.irCrawledUrlCount.ToDouble();//Useless now
                        AllVariables.blConitnue = false;//making it false (I used it before but now I am not ) (I commented it out and I will use it later)
                    }
                    string srPerMinCrawlingspeed = ((AllVariables.irCrawledUrlCount.ToDouble()) / ((DateTime.Now - AllVariables.dtStartDate).TotalMinutes+AllVariables.dblOldTime)).ToString("N2");//Crawling speed = crawlCount / minutes

                    string srPerMinDiscoveredLinkSpeed = ((AllVariables.irDiscoveredUrlCount.ToDouble()) / ((DateTime.Now -AllVariables.dtStartDate).TotalMinutes + AllVariables.dblOldTime)).ToString("N2");//discover Speed = discovercount/minutes
                    AllVariables.srPassedTime = ((DateTime.Now - AllVariables.dtStartDate).TotalMinutes + AllVariables.dblOldTime).ToString("N2");//Total timer
                    AllVariables.MainInformation.crawlSpeed = srPerMinCrawlingspeed;//inserting the info to public variables
                    AllVariables.MainInformation.TotalFoundUrls = AllVariables.irDiscoveredUrlCount.ToString();//inserting the info to public variables
                    AllVariables.MainInformation.TotalCrawledUrls = AllVariables.irCrawledUrlCount.ToString();//inserting the info to public variables
                    AllVariables.MainInformation.urlFindingSpeed = srPerMinDiscoveredLinkSpeed;//inserting the info to public variables
                    AllVariables.MainInformation.TotalTime = AllVariables.srPassedTime;//inserting the info to public variables
                    AllVariables.UserLogs.Insert(0, $"{DateTime.Now} polling awaiting urls \t processing: {AllVariables.blBeingProcessed} \t number of crawling tasks: {AllVariables.lstCrawlingTasks.Count}");//inserting user logs

                    AllVariables.UserLogs.Insert(0, $"Total Time: {AllVariables.srPassedTime} Minutes \t Total Crawled Links Count: {AllVariables.irCrawledUrlCount.ToString("N0")} \t Crawling Speed Per Minute: {srPerMinCrawlingspeed} \t Total Discovered Links : {AllVariables.irDiscoveredUrlCount.ToString("N0")} \t Discovered Url Speed: {srPerMinDiscoveredLinkSpeed} ");//inserting user logs

                    updatingInfo();//calling the update info (It wasn't working before delegate)
            }
            });

            OtherMethods.logMesssage($"polling awaiting urls \t processing: {AllVariables.blBeingProcessed} \t number of crawling tasks: {AllVariables.lstCrawlingTasks.Count}");
            if (AllVariables.blBeingProcessed)//don't enter if being processed by another task or thread
            {
                return;
            }
            lock (AllVariables._lock_CrawlingSync)
            {
                AllVariables.blBeingProcessed = true;//it's now being processed
                AllVariables.lstCrawlingTasks = AllVariables.lstCrawlingTasks.Where(pr => pr.Status != TaskStatus.RanToCompletion && pr.Status != TaskStatus.Faulted).ToList();
                int irTasksCountToStart = AllVariables.irNumberOfTotalConcurrentCrawling - AllVariables.lstCrawlingTasks.Count;//number of tasks
                if (irTasksCountToStart > 0)
                {
                    using (DBWebCrawlerDatabase context = new DBWebCrawlerDatabase())
                    {
                        
                        var vrReturnedList = context.CrawledUrlsTables.Where(x => (x.IsCrawled == false && x.BlockThisUrl == false && x.Failed == false) || (x.Failed == true && EntityFunctions.DiffHours(x.LastTryDate,DateTime.Now) > 24)).OrderBy(pr => pr.DiscoverDate).Select(x => new
                        {
                            x.UrlName,
                            x.LinkDepthLevel
                           ,
                            x.Failed,
                            x.ParentUrlHash,
                            x.TryCounter,
                            x.LastTryDate,
                            x.DiscoverDate
                        }).Take(irTasksCountToStart * 2).ToList();//entityFunctions // In here I selected all the urls that's not crawled and not blocked and not failed or we are selecting the failed ones that have time more than 24hrs
                        if (vrReturnedList.Count == 0)//if it is 0 then it didnt find anything
                        {
                            AllVariables.makeSureCounter++;//I am increasing it 5 times to make sure after that it ends

                            if (AllVariables.makeSureCounter == 5)//now it didnt find anything and it ended
                            {
                                AllVariables.timer.Stop();
                                MessageBox.Show($"Crawling This Site has ended. it tooke {AllVariables.MainInformation.TotalTime} Minutes and the discovered url speed was {AllVariables.MainInformation.urlFindingSpeed} and the crawl speed was {AllVariables.MainInformation.crawlSpeed} it discovered {AllVariables.MainInformation.TotalFoundUrls} urls and it crawled {AllVariables.MainInformation.TotalCrawledUrls} urls \n Click on okay to stop and reset");//END INFO
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {
                                    reset();
                                });//Reseting everything
                               
                            }

                        }

                        OtherMethods.logMesssage(string.Join(" , ", vrReturnedList.Select(pr => pr.UrlName)));//Logs
                        foreach (var vrPerReturned in vrReturnedList)
                        {

                            var vrUrlToCrawl = vrPerReturned.UrlName;
                            int irDepth = vrPerReturned.LinkDepthLevel;
                            var parenturl = vrPerReturned.ParentUrlHash;
                            var trycounter = vrPerReturned.TryCounter;
                            DateTime dtLastTryDate = vrPerReturned.LastTryDate;
                            DateTime dtDiscoverDate = vrPerReturned.DiscoverDate;
                            lock (AllVariables.lstCurrentlyCrawlingUrls)
                            {
                                if (AllVariables.lstCurrentlyCrawlingUrls.Contains(vrUrlToCrawl))
                                {
                                    OtherMethods.logMesssage($"bypass url since already crawling: \t {vrUrlToCrawl}");
                                    continue;
                                }
                                AllVariables.lstCurrentlyCrawlingUrls.Add(vrUrlToCrawl);
                            }
                            OtherMethods.logMesssage($"starting crawling url: \t {vrUrlToCrawl}");

                            lock (AllVariables.UserLogs)
                            {
                                App.Current.Dispatcher.Invoke((Action)delegate
                                {

                                    AllVariables.UserLogs.Insert(0, $"{DateTime.Now} starting crawling url: \t {vrUrlToCrawl}");
                                });
                            }
                            
                            var vrStartedTask = Task.Factory.StartNew(() => { AllMethods.Crawl(vrUrlToCrawl, irDepth, parenturl, dtDiscoverDate, trycounter, dtLastTryDate); }, TaskCreationOptions.LongRunning).ContinueWith((pr) =>
                            {
                                lock (AllVariables.lstCurrentlyCrawlingUrls)
                                {

                                    AllVariables.lstCurrentlyCrawlingUrls.Remove(vrUrlToCrawl);
                                    OtherMethods.logMesssage($"removing url from list since task completed: \t {vrUrlToCrawl}");
                                }
                            });
                            AllVariables.lstCrawlingTasks.Add(vrStartedTask);
                            if (AllVariables.lstCrawlingTasks.Count > AllVariables.irNumberOfTotalConcurrentCrawling)
                                break;
                        }
                    }
                }

                AllVariables.blBeingProcessed = false;//ended
            }

        }
        public void reset()//Reseting Main Info
        {
            AllVariables.MainInformation.crawlSpeed = "0";//Reseting Main Info
            AllVariables.MainInformation.TotalTime = "0";//Reseting Main Info
            AllVariables.MainInformation.crawlSpeed = "0";//Reseting Main Info
            AllVariables.MainInformation.TotalFoundUrls = "0";//Reseting Main Info
            AllVariables.MainInformation.TotalCrawledUrls = "0";//Reseting Main Info
            AllVariables.MainInformation.urlFindingSpeed = "0";//Reseting Main Info
            AllVariables.MainInformation.failedUrls = 0;
            AllVariables.MainInformation.totalBlocked = 0;
            label_CurrentUsedThreads.Content = "Current Status: ";
            AllVariables.irCrawledUrlCount = 0;//Reseting Main Info
            AllVariables.irDiscoveredUrlCount = 0;//Reseting Main Info
            AllVariables.srPassedTime = "0";//Reseting Main Info
            AllVariables.CrawledUrlCount = 0;//Reseting Main Info
            AllVariables.tempUrlCount = 0;//Reseting Main Info
            updatingInfo();//Updating The Info After the reset
        }
       
        
    }
}
