﻿using System;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Infrastructure;
using WorldYachts.Model;
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
        }
        public static void ShowWindow()
        {
            ((Window) new MainWindow()).Show();
        }
    }
}