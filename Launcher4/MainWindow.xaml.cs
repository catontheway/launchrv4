using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Launcher4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool siteOnline = false;
        private bool isUpdated = true;

        DateTime WstartTime, WendTime;

        WebClient webClient = new WebClient();

        Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download speed

        private void formBG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnclose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        // use async
        private async void Win1_Loaded(object sender, RoutedEventArgs e)
        {
            labelRealmlist.Content = string.Format("{0}", Config.realmlist);
            // start the waiting animation

            // simply start and await the loading task
            await Task.Run(() => CheckingSiteAvailability(1000));

            if (siteOnline)
            {
                if (File.Exists("Wow.exe"))
                    DownloadRequiredFiles();
                else
                {
                    isUpdated = false;
                    btn_play.Visibility = Visibility.Hidden;
                    MessageBox.Show("Can't download required files, please place the launcher inside the World of Warcraft folder!");
                }
            }
            else
            {
                labelSpeed.Content = "Update unavailable, carry on.";
                btn_play.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/btn_play_a.png"));
                isUpdated = true;
            }

            WstartTime = DateTime.Now;
            await Task.Run(() => CheckingWorldServerAvailability(2000));
            // all tasks finished
        }

        private void CheckingSiteAvailability(int sleepTime)
        {
            IsWebsiteOnline(sleepTime);
            Thread.Sleep(sleepTime);
        }

        private void CheckingWorldServerAvailability(int sleepTime)
        {
            IsWorldServerOnline(sleepTime);
            Thread.Sleep(sleepTime);
        }

        private void IsWebsiteOnline(int timeOutSecs)
        {
            if (MyFunctions.DoTryPing(Config.siteAddress, Config.sitePort, timeOutSecs))
                this.Dispatcher.Invoke(() => { siteOnline = true; });
            else
                this.Dispatcher.Invoke(() => { siteOnline = false; });
        }

        private void IsWorldServerOnline(int timeOutSecs)
        {
            if (MyFunctions.DoTryPing(Config.realmlist, Config.worldPort, timeOutSecs))
            {
                this.Dispatcher.Invoke(() =>
                {
                    WendTime = DateTime.Now;
                    Double elapsedMillisecs = ((TimeSpan)(WendTime - WstartTime)).TotalMilliseconds;

                    labelServerStatus.Content = string.Format("Server Online: {0}ms", Convert.ToInt32(elapsedMillisecs).ToString());
                    labelServerStatus.Foreground = Brushes.GreenYellow;
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    labelServerStatus.Content = "Wolrd Server is offline!";
                    labelServerStatus.Foreground = Brushes.OrangeRed;
                });
            }
        }

        private bool DownloadRequiredFiles()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Config.launcher_path + "/config.xml");
            try
            {
                foreach (XmlNode nodes in xmlDoc.SelectNodes("//file"))
                {
                    foreach (XmlAttribute attribute in nodes.Attributes)
                    {
                        if (attribute.Name == "folderTarget")
                        {
                            if (Directory.Exists(attribute.Value))
                            {
                                if (File.Exists(attribute.Value/*target folder name*/ + @"/" + nodes.InnerText/*filename*/))
                                {
                                    System.Net.WebRequest req = System.Net.HttpWebRequest.Create(Config.launcher_path + "/files/" + nodes.InnerText);

                                    req.Method = "HEAD";

                                    using (System.Net.WebResponse resp = req.GetResponse())
                                    {
                                        int ContentLength;

                                        if (int.TryParse(resp.Headers.Get("Content-Length"), out ContentLength))
                                        {
                                            FileInfo f1 = new FileInfo(attribute.Value/*target folder name*/ + @"/" + nodes.InnerText/*filename*/);

                                            if (ContentLength != f1.Length)
                                            {
                                                isUpdated = false;
                                                DownloadFile(Config.launcher_path + "/files/" + nodes.InnerText, attribute.Value + @"/" + nodes.InnerText);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    isUpdated = false;
                                    DownloadFile(Config.launcher_path + "/files/" + nodes.InnerText, attribute.Value + @"/" + nodes.InnerText);
                                }
                            }
                            else
                                MessageBox.Show("There is an error selecting the destination folder for the downloaded file!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Please Report the following error to our server:\n \n {0}", ex.ToString()));
                return false;
            }

            return true;
        }

        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // The variable that will be holding the url address (making sure it starts with http://)
                Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                // Start the stopwatch which we will be using to calculate the download speed
                sw.Start();

                try
                {
                    // Start downloading the file
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            labelSpeed.Content = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            // Update the progressbar percentage only when the value is not the same.
            pbarDownload.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            labelPerc.Content = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            labelDownloaded.Content = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));

            btn_play.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/btn_play_c.png"));
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            if (e.Cancelled == true)
            {
                isUpdated = false;
                labelDownloaded.Content = "Download has been canceled.";
            }
            else
            {
                isUpdated = true;

                labelDownloaded.Content = "Download completed.";

                btn_play.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/btn_play_a.png"));
            }
        }

        private void btn_play_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isUpdated)
                btn_play.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/btn_play_b.png"));
        }

        private void btn_play_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isUpdated)
                btn_play.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/btn_play_a.png"));
        }

        private void btn_play_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isUpdated)
            {
                using (var outputFile = new StreamWriter(@"WTF\Config.WTF", true))
                    outputFile.WriteLine("set realmlist " + Config.realmlist);

                if (Directory.Exists("Cache"))
                {
                    var dir = new DirectoryInfo("Cache");
                    dir.Delete(true); // true => recursive delete
                }

                string programPath = Directory.GetCurrentDirectory() + "\\Wow.exe";
                Process proc = new Process();
                proc.StartInfo.FileName = programPath;
                proc.Start();
                this.WindowState = WindowState.Minimized;
            }
        }
    }
}
