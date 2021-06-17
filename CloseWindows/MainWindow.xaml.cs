using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CloseWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenWindow window;
        private List<OpenWindow> openWindowsList;
        private Process[] processesArray;

        public MainWindow()
        {
            InitializeComponent();
            LstOpenWindows.ItemsSource = LoadTempWindows();
            //processArray = Process.GetProcesses();
            //processArray.Where( x => x.)
        }

        private List<OpenWindow> LoadTempWindows()
        {
            openWindowsList = new List<OpenWindow>();
            for (int i = 0; i < 4; i++)
            {
                window = new OpenWindow()
                {
                    Name = "Window " + i,
                    IsChecked = true
                };
                openWindowsList.Add(window);
            }

            return openWindowsList;
        }
       
        private void Close_All_Button(object sender, RoutedEventArgs e)
        {
            int amountOfWindowsClosed = 0;

            for (int i = 0; i < openWindowsList.Count; i++)
            {
                if (openWindowsList[i].IsChecked)
                {
                    openWindowsList.RemoveAt(i);
                    i--;

                    amountOfWindowsClosed++;
                }
            }

            LstOpenWindows.Items.Refresh();
        }
    }

    public class OpenWindow
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}