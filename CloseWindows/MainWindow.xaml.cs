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

            LstOpenWindows.ItemsSource = GetNewListOfOpenWindows();
        }
       
        private void Close_All_Button(object sender, RoutedEventArgs e)
        {
            int amountOfWindowsClosed = 0;

            for (int i = 0; i < openWindowsList.Count; i++)
            {
                if (openWindowsList[i].IsChecked)
                {
                    //openWindowsList[i].Kill();
                    // TODO : Add Another button to KILL all tasks immediately
                    openWindowsList[i].CloseWindow();
                    openWindowsList.RemoveAt(i);
                    i--;

                    amountOfWindowsClosed++;
                }
            }

            LstOpenWindows.Items.Refresh();
        }

        private List<OpenWindow> GetNewListOfOpenWindows()
        {
            processesList = Process.GetProcesses().
                   Where(x => x.MainWindowHandle != IntPtr.Zero).
                   ToList();

            openWindowsList = new List<OpenWindow>();

            foreach (Process process in processesList)
            {
                openWindowsList.Add(new OpenWindow { Process = process });
            }

            return openWindowsList;
        }
    }

    public class OpenWindow
    {
        public string WindowsName { get { return Process.ProcessName; } }
        public bool IsChecked { get; set; }
        public Process Process { get; set; }

        public OpenWindow()
        {
            IsChecked = true;
        }

        public void CloseWindow()
        {
            Process.CloseMainWindow();
        }
    }
}