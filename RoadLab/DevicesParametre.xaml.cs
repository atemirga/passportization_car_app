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
using System.IO.Ports;
using System.Globalization;
using System.Collections;


namespace RoadLab
{
    /// <summary>
    /// Interaction logic for DevicesParametre.xaml
    /// </summary>
    public partial class DevicesParametre : Window
    {
        
        public DevicesParametre()
        {
            InitializeComponent();
        }

        private void devicesCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void camera1UrlTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (camera1UrlTxt.Text == "rtsp://192.168.1.1:554")
            {
                camera1UrlTxt.Text = "";
                camera1UrlTxt.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void camera1UrlTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFCBB0B0"));
            if (String.IsNullOrWhiteSpace(camera1UrlTxt.Text))
            {
                camera1UrlTxt.Text = "rtsp://192.168.1.1:554";
                camera1UrlTxt.Foreground = mySolidColorBrush;
            }
            else {
                camera1UrlTxt.Foreground = new SolidColorBrush(Colors.Black);
            }
                
        }

        private void camera1UrlTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void camera2UrlTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (camera2UrlTxt.Text == "rtsp://192.168.1.2:554")
            {
                camera2UrlTxt.Text = "";
                camera2UrlTxt.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void camera2UrlTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFCBB0B0"));
            if (String.IsNullOrWhiteSpace(camera2UrlTxt.Text)) { 
                camera2UrlTxt.Text = "rtsp://192.168.1.2:554";
                camera2UrlTxt.Foreground = mySolidColorBrush;
            }
            else
            {
                camera2UrlTxt.Foreground = new SolidColorBrush(Colors.Black);
            }

        }

        private void camera1ConnBtn_Click(object sender, RoutedEventArgs e)
        {
            
           // MainWindow main_window = Owner as MainWindow();
            if (camera1ConnBtn.Content.ToString() == "Подключить")
            {
                var uri = new Uri(camera1UrlTxt.Text);
                camera1Stream.StartPlay(uri, TimeSpan.FromSeconds(15));
                
                
                camera1StatusLbl.Content = "Connecting...";
            }
            else {
                camera1Stream.Stop();
                (Application.Current.MainWindow as MainWindow).camera1Stream.Stop();
            }
        }

        private void camera2ConnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (camera2ConnBtn.Content.ToString() == "Подключить")
            {
                var uri = new Uri(camera2UrlTxt.Text);
                camera2Stream.StartPlay(uri, TimeSpan.FromSeconds(15));
                (Application.Current.MainWindow as MainWindow).camera2Stream.StartPlay(uri, TimeSpan.FromSeconds(15));
                camera2StatusLbl.Content = "Connecting...";
            }
            else
            {
                camera2Stream.Stop();
                (Application.Current.MainWindow as MainWindow).camera2Stream.Stop();
            }
        }

        private void UpdateButton1()
        {
            if (camera1Stream.IsPlaying)
            {
                camera1ConnBtn.Content = "Отключить";
            }
            else {
                camera1ConnBtn.Content = "Подключить";
            }
        }
        private void camera1StreamHandler(object sender, RoutedEventArgs e)
        {
            UpdateButton1();

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                camera1StatusLbl.Content = "Playing";
            }
            else if (e.RoutedEvent.Name == "StreamFailed")
            {
                camera1StatusLbl.Content = "Failed";

                MessageBox.Show(
                    ((WebEye.StreamFailedEventArgs)e).Error,
                    "Stream Player",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
                camera1StatusLbl.Content = "Stopped";
            }
        }

        private void UpdateButton2()
        {
            if (camera2Stream.IsPlaying)
            {
                camera2ConnBtn.Content = "Отключить";
            }
            else
            {
                camera2ConnBtn.Content = "Подключить";
            }
        }
        private void camera2StreamHandler(object sender, RoutedEventArgs e)
        {
            UpdateButton2();

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                camera2StatusLbl.Content = "Playing";
            }
            else if (e.RoutedEvent.Name == "StreamFailed")
            {
                camera2StatusLbl.Content = "Failed";

                MessageBox.Show(
                    ((WebEye.StreamFailedEventArgs)e).Error,
                    "Stream Player",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
                camera2StatusLbl.Content = "Stopped";
            }
        }

        
        private void cameraExecuteBtn_Click(object sender, RoutedEventArgs e)
        {
            var uri1 = new Uri(camera1UrlTxt.Text);
            (Application.Current.MainWindow as MainWindow).camera1Stream.StartPlay(uri1, TimeSpan.FromSeconds(15));

            var uri2 = new Uri(camera2UrlTxt.Text);
            (Application.Current.MainWindow as MainWindow).camera2Stream.StartPlay(uri2, TimeSpan.FromSeconds(15));

        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (var p in ports)
            {
                comPorts.Items.Add(p);
            }
            
            baudRates.Items.Add(9600);
            baudRates.Items.Add(57600);
            baudRates.Items.Add(115200);
            
        }
        private void hideProgressBar()
        {
            this.Dispatcher.Invoke((Action)(() => {
                insProgress.Visibility = Visibility.Hidden;
                connectedLbl.Visibility = Visibility.Visible;
            }));
        }
        private async void insOn_Click(object sender, RoutedEventArgs e)
        {
            SerialPort mySerialPort = new SerialPort(comPorts.SelectedItem.ToString());

            mySerialPort.BaudRate = Convert.ToInt32(baudRates.SelectedItem.ToString());
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;

            mySerialPort.Open();
            byte[] bytestosend_start = { 0xAA, 0x55, 0x00, 0x00, 0x07, 0x00, 0x52, 0x59, 0x00 };//start
            mySerialPort.Write(bytestosend_start, 0, bytestosend_start.Length);

            //wait for connection
            var progress = new Progress<int>(value => insProgress.Value = value);
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    ((IProgress<int>)progress).Report(i);
                    Thread.Sleep(300);
                    if (i == 99)
                    {
                        hideProgressBar();
                        
                    }
                }
            });

            int bytes = mySerialPort.BytesToRead;
            byte[] buffer = new byte[bytes];
            mySerialPort.Read(buffer, 0, bytes);
            string text = "AA-55-01-52";
            int from = BitConverter.ToString(buffer).IndexOf(text) + text.Length;
            int to = BitConverter.ToString(buffer).IndexOf(text, BitConverter.ToString(buffer).IndexOf(text) + 1) - 1;

            string result = "AA-55-01-52" + BitConverter.ToString(buffer).Substring(from, to - from);

            string[] holder = result.Split('-');
            var c = holder.Select(s => byte.Parse(s, NumberStyles.AllowHexSpecifier)).ToArray();

            byte[] bblat = new byte[4];
            Array.Copy(c, 36, bblat, 0, 4);//30

            byte[] bblong = new byte[4];
            Array.Copy(c, 40, bblong, 0, 4);//34

            byte[] bbalt = new byte[4];
            Array.Copy(c, 44, bbalt, 0, 4);//38

           
            byte[] temperature = new byte[2];
            Array.Copy(c, 34, temperature, 0, 2);//0

            string lat = BitConverter.ToInt32(bblat, 0).ToString();
            string lon = BitConverter.ToInt32(bblong, 0).ToString();
            string alt = BitConverter.ToInt32(bbalt, 0).ToString();


            if (lat[0] == '-')
            {
                latitude.Content = lat.Insert(3, ",");
            }
            else {
                latitude.Content = lat.Insert(2, ",");
            }
            if (lon[0] == '-')
            {
                longitude.Content = lon.Insert(3, ",");
            }
            else
            {
                longitude.Content = lon.Insert(2, ",");
            }
            if (alt[0] == '-')
            {
                altitude.Content = alt.Insert(3, ",");
            }
            else
            {
                altitude.Content = alt.Insert(2, ",");
            }
            
           
        }
        public  void Stop()
        {
            SerialPort mySerialPort = new SerialPort(comPorts.SelectedItem.ToString());

            byte[] bytestosend_start = { 0xAA, 0x55, 0x00, 0x00, 0x07, 0x00, 0xFE, 0x05, 0x01 };//start
            mySerialPort.Write(bytestosend_start, 0, bytestosend_start.Length);
            mySerialPort.Close();
        }
    }
}
