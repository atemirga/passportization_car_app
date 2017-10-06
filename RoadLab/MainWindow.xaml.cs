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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Timers;
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using AForge.Video;
using System.Drawing;
using System.IO;

namespace RoadLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MJPEGStream stream;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);

        }

        private void main_Initialized(object sender, EventArgs e)
        {
            CheckDevices();
            stopWatch.Start();
            dt.Start();

           // stream.Login = "admin";
            //stream.Password = "pretendtobe1234";
           // stream.ForceBasicAuthentication = true;
            //stream.RequestTimeout = 10000;
            //stream.Source = "http://192.168.0.33/nphMotionJpeg?Resolution=320x240&Quality=Standard";
            //stream.Start();
            
            

        }
        
        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (Environment.HasShutdownStarted)
            {
                MessageBox.Show("Total amount of time the system logged on:" + timeTxtBox.Text);
            }

        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Hours, ts.Minutes, ts.Seconds);
                timeTxtBox.Text = currentTime;
            }
        }
        public void CheckDevices()
        {
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            
            StartRoad sr = new StartRoad();
            sr.Show();
        }

        private void devicesMenu_Click(object sender, RoutedEventArgs e)
        {
            DevicesParametre dp = new DevicesParametre();
            dp.Show();
        }

        private void izmerenieMenu_Click(object sender, RoutedEventArgs e)
        {
            if (kallibrovkaMenu.IsChecked == true)
            {
                kallibrovkaMenu.IsChecked = false;
                izmerenieMenu.IsChecked = true;
            }
        }

        private void kallibrovkaMenu_Click(object sender, RoutedEventArgs e)
        {

            if (izmerenieMenu.IsChecked == true)
            {
                kallibrovkaMenu.IsChecked = true;
                izmerenieMenu.IsChecked = false;
            }
        }

        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void interfaceMenu_Click(object sender, RoutedEventArgs e)
        {
            InterfaceParametre ip = new InterfaceParametre();
            ip.Show();
        }

        public void getCamera()
        {

        }

        private void camera1_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void addTextBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Warning", "Text", MessageBoxButton.OK);
        }
    }
}
