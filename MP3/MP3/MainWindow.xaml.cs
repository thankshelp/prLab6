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
using System.Media;
using System.Windows.Controls.Primitives;
using System.Security;

namespace MP3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer;
        Dictionary<string, string> list = new Dictionary<string, string>();
        MediaPlayer player = new MediaPlayer();
        TimeSpan ts;
        int n = -1, q;
        string nm;
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        bool daHotKak = false;

        public MainWindow()
        {
            InitializeComponent();

            player.MediaOpened += Player_MediaOpened;

            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
               

        }


        private void Player_MediaOpened(object sender, EventArgs e)
        {
            vol.Maximum = 100.0;
            
            player.Volume = vol.Value/100.0;
            slid.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            slid.Value = 0;
            tv.Content = "0:0:0";
            dlina.Content = player.NaturalDuration.TimeSpan.Hours + ":" + player.NaturalDuration.TimeSpan.Minutes + ":" + player.NaturalDuration.TimeSpan.Seconds;

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (daHotKak == false)
            {
                tv.Content = player.Position.Hours + ":" + player.Position.Minutes + ":" + player.Position.Seconds;
                slid.Value = player.Position.TotalSeconds;
                if (player.Position == player.NaturalDuration)
                {
                    if (rand.IsChecked != true)
                    {
                        if (stack.SelectedIndex != n)
                        {
                            stack.SelectedIndex++;
                            nm = stack.Items[stack.SelectedIndex].ToString();
                            string b = list[nm];
                            player.Open(new Uri(b, UriKind.Relative));
                            player.Play();
                            Timer.Start();
                            tv.Content = "0:0:0";
                            
                        }
                        else
                        {
                            Timer.Stop();
                            player.Stop();
                        }                           
                    }
                    else
                    {
                        while (q == stack.SelectedIndex)
                        {
                            q = new Random().Next(0, n);
                        }
                        stack.SelectedIndex = q;
                            nm = stack.Items[stack.SelectedIndex].ToString();
                            string b = list[nm];
                            player.Open(new Uri(b, UriKind.Relative));
                            player.Play();
                            Timer.Start();
                            tv.Content = "0:0:0";
                        
                    }
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Play();
                Timer.Start();
                tv.Content = "0:0:0";
                dlina.Content = player.NaturalDuration.TimeSpan.Hours + ":" + player.NaturalDuration.TimeSpan.Minutes + ":" + player.NaturalDuration.TimeSpan.Seconds;
            }
            catch 
            {
                q = new Random().Next(0, n);

                stack.SelectedIndex = q;
                nm = stack.Items[stack.SelectedIndex].ToString();
                string b = list[nm];
                player.Open(new Uri(b, UriKind.Relative));
                player.Play();
                Timer.Start();
                tv.Content = "0:0:0";
                
            }
           
        }

        private void Stack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nm = stack.Items[stack.SelectedIndex].ToString();
            string b = list[nm];
            player.Open(new Uri(b, UriKind.Relative));
            //time = player.NaturalDuration;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            Timer.Stop();
            dlina.Content = "";
            tv.Content = "";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;

            dlg.ShowDialog();

            foreach (string s in dlg.FileNames)
            {
                n++;
                //nm = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);

                nm = System.IO.Path.GetFileNameWithoutExtension(s);
                list.Add(nm, s);

                stack.Items.Add(nm);
            }
        }

        private void volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float sliderValue = (float)vol.Value;
            player.Volume = sliderValue/100.0;
        }
        
        private void slid_DragStarted(object sender, DragStartedEventArgs e)
        {
            daHotKak = true;
        }

        private void slid_DragEnded(object sender, DragCompletedEventArgs e)
        {
            int sliderValue = (int)slid.Value;

            ts = new TimeSpan(0, 0, sliderValue);
            player.Position = ts;
            daHotKak = false;
        }

        private void slid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int)slid.Value;

            tv.Content = (sliderValue / 3600).ToString() + ":" + (sliderValue / 60).ToString() + ":" + (sliderValue % 60).ToString();
        }

        
    }
}
