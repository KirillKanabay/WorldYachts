using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Infrastructure;
using WorldYachts.Data;
using WorldYachts.View;

namespace WorldYachts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HeaderPanel.MouseDown += DragWindow;
            MinimizeBtn.Click += (s, e) => WindowState = WindowState.Minimized;
            CloseBtn.Click += (s,e) => Application.Current.Shutdown();
        }
        public static void ShowWindow()
        {
            ((Window) new MainWindow()).Show();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == 0) { DragMove(); }
        }

        public void SendSnackbar(string message)
        {
            var messageQueue = Snackbar.MessageQueue;
            Task.Run(()=>messageQueue.Enqueue(message, 
                "Закрыть",
                (param)=> Trace.WriteLine("Закрыто"), 
                message));
        }
    }
}
