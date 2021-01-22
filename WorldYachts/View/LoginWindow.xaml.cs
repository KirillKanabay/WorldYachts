using System;
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
using WorldYachts.Model;

namespace WorldYachts.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static event Action<bool> LoginEvent; 
        public LoginWindow()
        {
            InitializeComponent();
            var worldYachtsContext = WorldYachtsContext.GetDataContext();
            LoginEvent += CheckLoginResult;
        }

        private void CheckLoginResult(bool result)
        {
            if (result)
            {
                MainWindow.ShowWindow();
            }
        }

        public static void CheckLoginResultEvent(bool result)
        {
            LoginEvent?.Invoke(result);
        }
    }
}
