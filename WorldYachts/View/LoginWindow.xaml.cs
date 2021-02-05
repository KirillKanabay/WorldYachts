﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldYachts.Infrastructure;
using WorldYachts.Data;

namespace WorldYachts.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static Action CloseWindow;
        public LoginWindow()
        {
            InitializeComponent();
            CloseWindow += Close;
        }
        public static void ShowWindow()
        {
            ((Window)new LoginWindow()).Show();
        }
    }
}
