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
            RefreshOpenWindowsItems();

            chkBox_SelectAll.IsChecked = true;
            chkBox_ShowStartTime.IsChecked = true;
        }

        #region Event Handlers
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (openWindowsList == null)
            {
                return;
            }

            openWindowsList.ForEach(x => x.IsChecked = true);
            RefreshOpenWindowsItems();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (openWindowsList == null)
            {
                return;
            }

            openWindowsList.ForEach(x => x.IsChecked = false);
            RefreshOpenWindowsItems();
        }

        private void ChkBox_StartTime_Checked(object sender, RoutedEventArgs e)
        {
            gvColumn_StartTime.Width = 200;
        }

        private void ChkBox_StartTime_Unchecked(object sender, RoutedEventArgs e)
        {
            gvColumn_StartTime.Width = 0;
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

            RefreshOpenWindowsItems();
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            LstOpenWindows.ItemsSource = GetNewListOfOpenWindows();
            RefreshOpenWindowsItems();
        }

        #endregion

        #region Private Methods

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

        private void RefreshOpenWindowsItems()
        {
            LstOpenWindows.Items.Refresh();
            gridViewColOne.Header = "Application (" + openWindowsList.Count + ")";
        }

        #endregion
    }

    public class OpenWindow
    {
        public string WindowsName { get { return Process.ProcessName; } }
        public DateTime StartTime { get { return Process.StartTime; } }
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