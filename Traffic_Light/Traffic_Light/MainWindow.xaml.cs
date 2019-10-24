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
using System.Windows.Threading;

namespace Traffic_Light
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Color RED = Color.FromRgb(255, 0, 0);
        public Color AMBER = Color.FromRgb(255, 162, 0);
        public Color GREEN = Color.FromRgb(0, 255, 0);
        public Color BLACK = Color.FromRgb(0, 0, 0);

        DispatcherTimer timer = new DispatcherTimer();

        private bool running = false;
        private EventHandler RedToGreen;
        private EventHandler GreenToRed;

        private byte counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            RedToGreen = new EventHandler(dispatcherTimer_Tick);
            GreenToRed = new EventHandler(dispatcherTimer_Tick2);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!running && ((SolidColorBrush) RedLight.Fill).Color == RED)
            {
                running = true;
                timer.Tick += RedToGreen;
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
                counter = 0;
            }
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (counter == 1)
            {
                AmberLight.Fill = new SolidColorBrush(AMBER);
                counter++;
            } else if (counter == 1)
            {
                RedLight.Fill = new SolidColorBrush(BLACK);
                AmberLight.Fill = new SolidColorBrush(BLACK);
                GreenLight.Fill = new SolidColorBrush(GREEN);
                timer.Stop();
                timer.Tick -= RedToGreen;
                running = false;
            }
        }

        public void dispatcherTimer_Tick2(object sender, EventArgs e)
        {
            switch (counter)
            {
                case 0:
                    GreenLight.Fill = new SolidColorBrush(BLACK);
                    AmberLight.Fill = new SolidColorBrush(AMBER);
                    counter++;
                    break;
                case 1:
                    RedLight.Fill = new SolidColorBrush(RED);
                    AmberLight.Fill = new SolidColorBrush(BLACK);
                    timer.Stop();
                    timer.Tick -= GreenToRed;
                    running = false;
                    break;
            }
        }
    }
}
