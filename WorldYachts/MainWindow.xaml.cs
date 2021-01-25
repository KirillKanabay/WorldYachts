using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
        }
        public static void ShowWindow()
        {
            ((Window) new MainWindow()).Show();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 0;
        }

        private void Catalog_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 2;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 3;
        }
        private void CatalogManagement_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 4;
        }
        private void AccountSettings_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 6;
        }
        private void UserManagement_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 7;
        }

        private void InfoMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            Menu.SelectedIndex = 11;
        }

        
    }
}
