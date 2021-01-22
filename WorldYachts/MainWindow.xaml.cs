using System;
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
        public static event Action Close;
        public static event Action Show;
        public static WorldYachtsContext WorldYachtsContext;
        public MainWindow()
        {
            InitializeComponent();

            Close += () =>
            {
                base.Hide();
            };
            Show += () =>
            {
                base.Show();
            };
        }
        public static void CloseWindow()
        {
            Close?.Invoke();
        }

        public static void ShowWindow()
        {
            ((Window) new MainWindow()).Show();
        }
        // public static WorldYachtsContext GetDataContext()
        // {
        //     var optionsBuilder = new DbContextOptionsBuilder<WorldYachtsContext>();
        //
        //     var options = optionsBuilder
        //         .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=worldyachtsdb;Trusted_Connection=True;")
        //         .Options;
        //     return new WorldYachtsContext(options);
        // }
    }
}
