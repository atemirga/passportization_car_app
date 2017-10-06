using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using Microsoft.Win32;
using AviFile;
using System.Drawing;
using AForge.Video.FFMPEG;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace RoadLab
{
    /// <summary>
    /// Interaction logic for StartRoad.xaml
    /// </summary>
    public partial class StartRoad : Window
    {
        public StartRoad()
        {
            InitializeComponent();
        }

        private void laneStartRoadCombo_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> lanes = new List<int>();
            lanes.Add(1);
            lanes.Add(2);
            lanes.Add(3);
            lanes.Add(4);
            lanes.Add(5);

            var combo = sender as ComboBox;
            combo.ItemsSource = lanes;
            combo.SelectedIndex = 0;

            
        }

        private void laneStartRoadCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            
        }

        private void closeStartRoadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pathStartRoadBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog().ToString().Equals("OK"))
            {
                pathStartRoadTxtBox.Text = fbd.SelectedPath;
            }
        }
        private void startRoadBtn_Click(object sender, RoutedEventArgs e)
        {
            var _mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w is MainWindow) as MainWindow;


            List<string> cameras = new List<string>();
            for (int i = 1; i <= 2; i++)
            {
                cameras.Add("camera_" + i);
            }

            List<string> videos = new List<string>();

            for (int i = 0; i < cameras.Count; i++)
            {
                videos.Add(XmlReade(cameras.ElementAt(i)));
            }

            Parallel.Invoke(
                () => Camera1(videos.ElementAt(0)),
                () => Camera2(videos.ElementAt(1)));
        }

        private static void Camera1(string video_name)
        {
            int c = 1;
            string name = "";
            string ip = "192.168.1.71";
            string username = "admin";
            string password = "1234";
            string path = "";
            bool check = false;
            if (username != "" & password != "")
            {
                path = "rtsp://" + username + ":" + password + "@" + ip + ":554";
            }
            else
            {
                path = "rtsp://" + ip + ":554";
            }


            name = video_name;

            Process grabInfoProcess;
            grabInfoProcess = new Process();
            grabInfoProcess.StartInfo.UseShellExecute = false;
            grabInfoProcess.StartInfo.RedirectStandardOutput = true;
            grabInfoProcess.StartInfo.FileName = "ffmpeg.exe";
            grabInfoProcess.StartInfo.Arguments = "-i rtsp://" + username + ":" + password + "@" + ip + ":554 -b 400k -vcodec copy -r 60 -y " + name + ".avi";
            grabInfoProcess.Start();
            string output = grabInfoProcess.StandardOutput.ReadToEnd();
            grabInfoProcess.WaitForExit();
            Thread.Sleep(0);

        }
        private static void Camera2(string video_name)
        {
            int c = 2;
            string name = "";
            string ip = "192.168.1.65";
            string username = "";
            string password = "";
            string path = "";
            if (username != "" & password != "")
            {
                path = "rtsp://" + username + ":" + password + "@" + ip + ":554";
            }
            else
            {
                path = "rtsp://" + ip + ":554";
            }
            name = video_name;

            Process grabInfoProcess;
            grabInfoProcess = new Process();
            grabInfoProcess.StartInfo.UseShellExecute = false;
            grabInfoProcess.StartInfo.RedirectStandardOutput = true;
            grabInfoProcess.StartInfo.FileName = "ffmpeg.exe";
            grabInfoProcess.StartInfo.Arguments = "-i " + path + " -b 400k -vcodec copy -r 60 -y " + name + ".avi";
            grabInfoProcess.Start();
            string output = grabInfoProcess.StandardOutput.ReadToEnd();
            grabInfoProcess.WaitForExit();
            Thread.Sleep(0);
        }

        public static string XmlReade(string camera_name)
        {
            bool check = false;
            int v = 1;
            string video_name = camera_name + "_" + v;

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\camera.xml");
            XmlNodeList elemList = doc.GetElementsByTagName("VIDEO_NAME");

            for (int i = 0; i < elemList.Count; i++)
            {
                //check = true;
                if (elemList[i].InnerXml.StartsWith(camera_name))
                {
                    if (elemList[i].InnerXml == video_name)
                    {
                        v++;
                    }
                    else if (elemList[i].InnerXml == camera_name + "_" + v)
                    {
                        v++;
                    }
                    check = true;
                }
            }
            if (check == true)
            {
                video_name = camera_name + "_" + v; ;
                XmlElement foo = doc.CreateElement("VIDEO_NAME");
                foo.InnerText = video_name;
                doc.DocumentElement.AppendChild(foo);
                doc.Save("camera.xml");
            }

            if (check == false)
            {
                XmlElement foo = doc.CreateElement("VIDEO_NAME");
                foo.InnerText = video_name;
                doc.DocumentElement.AppendChild(foo);
                doc.Save("camera.xml");

            }
            return video_name;
        }
    }
}
