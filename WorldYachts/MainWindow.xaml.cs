using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
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

            HeaderPanel.MouseDown += (s,e)=>
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
                DragWindow(s,e);
            };
            MinimizeBtn.Click += (s, e) => WindowState = WindowState.Minimized;

            WindowStateBtn.Click += (s, e) =>
            {
                WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
                WindowStateBtn.ToolTip = (WindowState == WindowState.Maximized) ? "Восстановить" : "Развернуть";
                WindowStateIcon.Kind = (WindowState == WindowState.Maximized) ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
            };
                
            
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
