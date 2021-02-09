using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorldYachts.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public static Action CloseWindow;
        public RegisterWindow()
        {
            InitializeComponent();
            CloseWindow += Close;
            HeaderPanel.MouseDown += (s, e) =>
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                }
                DragWindow(s, e);
            };
            MinimizeBtn.Click += (s, e) => WindowState = WindowState.Minimized;

            CloseBtn.Click += (s, e) => Application.Current.Shutdown();
        }
        public static void ShowWindow()
        {
            ((Window)new RegisterWindow()).Show();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == 0) { DragMove(); }
        }
    }
}
