using System;
using System.Runtime.CompilerServices;
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
    }
}
