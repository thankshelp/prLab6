 using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MP4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer;
        TimeSpan ts;
        bool v = false;

        public MainWindow()
        {
            InitializeComponent();

            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void vid_MediaOpened(object sender, RoutedEventArgs e)
        {
            vol.Maximum = 100.0;
            vid.Volume = vol.Value / 100.0;
            slid.Maximum = vid.NaturalDuration.TimeSpan.TotalSeconds;
            slid.Value = 0;
            dlina.Content = vid.NaturalDuration.TimeSpan.Hours + ":" + vid.NaturalDuration.TimeSpan.Minutes + ":" + vid.NaturalDuration.TimeSpan.Seconds;
            tv.Content = "0:0:0";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (v == false)
            {
                tv.Content = vid.Position.Hours + ":" + vid.Position.Minutes + ":" + vid.Position.Seconds;
                slid.Value = vid.Position.TotalSeconds;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            vid.Source = new Uri(dlg.FileName, UriKind.Relative);
            
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            vid.Play();
            Timer.Start();
            
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            vid.Pause();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            vid.Stop();
            Timer.Stop();
            dlina.Content = "";
            tv.Content = "";
        }

       
        private void slid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int)slid.Value;

            tv.Content = (sliderValue / 3600).ToString() + ":" + (sliderValue / 60).ToString() + ":" + (sliderValue % 60).ToString();
        }

        private void slid_ValueStarted(object sender, DragStartedEventArgs e)
        {
            v = true;
        }

        private void slid_ValueCompleted(object sender, DragCompletedEventArgs e)
        {
            int sliderValue = (int)slid.Value;

            ts = new TimeSpan(0, 0, sliderValue);
            vid.Position = ts;
            v = false;
        }

        private void Vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float sliderValue = (float)vol.Value;
            vid.Volume = sliderValue / 100.0;
        }
    }
}
