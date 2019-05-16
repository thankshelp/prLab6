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

        //private void InitializeOpenFileDialog()
        //{
            
        //    this.dlg.Filter = "MP3 (*.MP3)|*.MP3";
        //    this.dlg.Multiselect = true;
        //    this.dlg.Title = "My Music Browser";
        //}

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            
            slid.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            slid.Value = 0;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (daHotKak == false)
            {
                tv.Content = player.Position.Hours + ":" + player.Position.Minutes + ":" + player.Position.Seconds;
                slid.Value = player.Position.TotalSeconds;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            Timer.Start();
            dlina.Content = player.NaturalDuration.TimeSpan.Hours + ":" + player.NaturalDuration.TimeSpan.Minutes + ":" + player.NaturalDuration.TimeSpan.Seconds;
            tv.Content = "0:0:0";
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

            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.ShowDialog();
            //InitializeOpenFileDialog();

          
            //DialogResult dr = this.dlg.ShowDialog();
            //if (dr == System.Windows.Forms.DialogResult.OK)
            //{
            //    // Read the files
            //    foreach (String file in dlg.FileNames)
            //    {
            //        // Create a PictureBox.
            //        try
            //        {
                        nm = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                        list.Add(nm, dlg.FileName);
                        stack.Items.Add(nm);
            //        }
            //        catch (SecurityException ex)
            //        {
            //            // The user lacks appropriate permissions to read files, discover paths, etc.
            //            MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
            //                "Error message: " + ex.Message + "\n\n" +
            //                "Details (send to Support):\n\n" + ex.StackTrace
            //            );
            //        }
            //        catch (Exception ex)
            //        {
            //            // Could not load the image - probably related to Windows file system permissions.
            //            MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
            //                + ". You may not have permission to read the file, or " +
            //                "it may be corrupt.\n\nReported error: " + ex.Message);
            //        }
            //    }

            //}
        }

        private void volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int)volume.Value;
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
