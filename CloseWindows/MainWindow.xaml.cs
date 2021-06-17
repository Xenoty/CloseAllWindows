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
        private List<OpenWindow> openWindowsList;
        private List<Process> processesList;

        public MainWindow()
        {
            InitializeComponent();
            processesList = Process.GetProcesses().Where(x => x.MainWindowHandle != IntPtr.Zero).ToList();

            openWindowsList = new List<OpenWindow>();

            foreach (Process process in processesList)
            {
                openWindowsList.Add(new OpenWindow { WindowsName = process.ProcessName });
            }

            LstOpenWindows.ItemsSource = openWindowsList;
        }
       
        private void Close_All_Button(object sender, RoutedEventArgs e)
        {
            int amountOfWindowsClosed = 0;

            for (int i = 0; i < openWindowsList.Count; i++)
            {
                if (openWindowsList[i].IsChecked)
                {
                    //processesList[i].Kill();
                    // TODO : Add Another button to KILL all tasks immediately
                    processesList[i].CloseMainWindow();
                    openWindowsList.RemoveAt(i);
                    i--;

                    amountOfWindowsClosed++;
                }
            }

            LstOpenWindows.Items.Refresh();
        }
    }

    public class OpenWindow : Process
    {
        public OpenWindow()
        {
            IsChecked = true;
        }
        public string WindowsName { get; set; }
        public bool IsChecked { get; set; }
    }
}