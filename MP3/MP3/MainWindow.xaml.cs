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
        MediaPlayer player = new MediaPlayer();
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
            
        }

        private void Stack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nm = stack.Items[stack.SelectedIndex].ToString();
            player.Open(new Uri(nm, UriKind.Relative));
            time = player.NaturalDuration;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            player.Close();
        }
    }
}
