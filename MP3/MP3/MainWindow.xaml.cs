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

namespace MP3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        MediaPlayer player = new MediaPlayer();
        TimeSpan ts;
        string nm;
        Duration time;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            dlina.Content = time;
            tv.Content = player.Position;
        }

        private void Stack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nm = stack.Items[stack.SelectedIndex].ToString();
            string b = list[nm];
            player.Open(new Uri(b, UriKind.Relative));
            time = player.NaturalDuration;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            dlina.Content = "";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
           
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            
            nm = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
            list.Add(nm, dlg.FileName);
            stack.Items.Add(nm);

        }

        private void volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void slid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int)slid.Value;

            ts = new TimeSpan(0, 0, 0, 0, sliderValue);
            player.Position = ts;
        }

    }
}
