using System;
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
            processesArray = Process.GetProcesses().Where(x => x.MainWindowHandle != IntPtr.Zero).ToArray();
            LstOpenWindows.ItemsSource = processesArray;
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
        public string WindowsName { get; set; }
        public bool IsChecked { get; set; }
    }
}